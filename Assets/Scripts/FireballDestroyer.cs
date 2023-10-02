using UnityEngine;

public class FireballDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Fireball>(out Fireball fireball))
            Destroy(fireball.gameObject);
    }
}
