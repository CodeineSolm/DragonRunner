using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyAnimations))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _basicSpeed;
    [SerializeField] private float _deathDelayTime;
    [SerializeField] private int _damage;
    [SerializeField] private int _points;

    public event UnityAction<int> Died;

    private EnemyAnimations _animations;
    private CircleCollider2D _circleCollider2D;
    private float _speed;

    public void Reset()
    {
        _circleCollider2D.enabled = true;
        _speed = _basicSpeed;
    }

    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _animations = GetComponent<EnemyAnimations>();
        Reset();
    }

    private void Update()
    {
        Run();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Fireball>(out Fireball fireball))
        {
            Died?.Invoke(_points);
            StartCoroutine(Die());
        }
        else if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.ApplyDamage(_damage);
            StartCoroutine(Die());
        }
    }

    private void Run()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private IEnumerator Die()
    {
        _circleCollider2D.enabled = false;
        _speed = 0;
        _animations.Die();
        yield return new WaitForSeconds(_deathDelayTime);
        gameObject.SetActive(false);
    }
}
