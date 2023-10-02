using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FireballAnimations))]
[RequireComponent(typeof(CircleCollider2D))]
public class Fireball : MonoBehaviour
{
    [SerializeField] private AudioSource _soundEffect;
    [SerializeField] private float _speed;
    [SerializeField] private float _deathDelay = 1f;

    private FireballAnimations _animations;
    private CircleCollider2D _circleCollider;

    private void Start()
    {
        _animations = GetComponent<FireballAnimations>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.enabled = true;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _speed = 0;
        _circleCollider.enabled = false;
        _soundEffect.Play();
        _animations.Explosion();
        yield return new WaitForSeconds(_deathDelay);
        Destroy(gameObject);
    }
}
