using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSoundEffects _soundEffects;
    [SerializeField] private int _health;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private Fireball _fireBallTemplate;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _maxShootCooldownTime;
    [SerializeField] private float _attackDelay = 0.5f;
    [SerializeField] private float _jumpDelay = 1f;
    [SerializeField] private float _dieDelay = 1.2f;
    [SerializeField] private float _hurtDelay = 0.5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private FireballButton _fireballButton;
    [SerializeField] private JumpButton _jumpButton;

    public event UnityAction<int> HealthChanged;
    public event UnityAction Died;
    public event UnityAction<float> Shooted;

    private Rigidbody2D _rigidbody;
    private PlayerAnimations _animations;
    private CircleCollider2D _circleCollider;
    private bool _isAttacking;
    private bool _isJumping;
    private bool _isDying;
    private bool _isHurting;
    private float _shootCooldownTime = 0;

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health);

        if (_health <= 0)
            StartCoroutine(Die());
        else
            StartCoroutine(Hurt());
    }

    private void OnEnable()
    {
        _fireballButton.FireballButtonClicked += OnFireballButtonClick;
        _jumpButton.JumpButtonClicked += OnJumpButtonClick;
    }

    private void OnDisable()
    {
        _fireballButton.FireballButtonClicked -= OnFireballButtonClick;
        _jumpButton.JumpButtonClicked -= OnJumpButtonClick;
    }

    private void Start()
    {
        _isDying = false;
        _isHurting = false;
        _isJumping = false;
        _isAttacking = false;
        HealthChanged?.Invoke(_health);
        _rigidbody = GetComponent<Rigidbody2D>();
        _animations = GetComponent<PlayerAnimations>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (!_isAttacking && !_isJumping && !_isDying && !_isHurting)
            Run();

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && !_isDying)
            StartCoroutine(Jump());

        if (Input.GetMouseButtonDown(0) && !_isDying && _shootCooldownTime <= 0 && !_isAttacking)
            StartCoroutine(Attack());

        if (_shootCooldownTime > 0)
            _shootCooldownTime -= Time.deltaTime;
    }

    private void Run()
    {
        _animations.Run();
        transform.Translate(-transform.position.x * _speed * Time.deltaTime, 0, 0);
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Linecast(transform.position, _groundCheck.position, _groundLayerMask);
        return raycastHit.collider != null;
    }

    private IEnumerator Jump()
    {
        _soundEffects.Jump();
        _isJumping = true;
        _animations.Jump();
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
        yield return new WaitForSeconds(_jumpDelay);
        _isJumping = false;
    }

    private IEnumerator Hurt()
    {
        _soundEffects.Hurt();
        _isHurting = true;
        _animations.Hurt();
        yield return new WaitForSeconds(_hurtDelay);
        _isHurting = false;
    }

    private IEnumerator Attack()
    {
        _soundEffects.Shoot();
        Shooted?.Invoke(_maxShootCooldownTime);
        _isAttacking = true;
        StartCoroutine(ShootFireball());
        _animations.Attack();
        _shootCooldownTime = _maxShootCooldownTime;
        yield return new WaitForSeconds(_attackDelay);
        _isAttacking = false;
    }

    private IEnumerator ShootFireball()
    {
        yield return null;
        Instantiate(_fireBallTemplate, _shootPoint.position, Quaternion.identity);
    }

    private IEnumerator Die()
    {
        _circleCollider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
        _soundEffects.Death();
        _isDying = true;
        _animations.Die();
        yield return new WaitForSeconds(_dieDelay);
        Died?.Invoke();
    }

    private void OnFireballButtonClick()
    {
        if (_shootCooldownTime <= 0 && !_isAttacking && !_isDying)
            StartCoroutine(Attack());
    }

    private void OnJumpButtonClick()
    {
        if (IsGrounded() && !_isDying)
            StartCoroutine(Jump());
    }
}
