using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimations : MonoBehaviour
{
    private Animator _animator;

    public void Die()
    {
        _animator.SetTrigger(AnimatorEmemyController.States.Die);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
