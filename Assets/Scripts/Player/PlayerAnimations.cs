using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;
    private string _currentAnimation;

    public void Jump()
    {
        ChangeAnimationState(AnimatorPlayerController.States.Jump);
    }

    public void Attack()
    {
        ChangeAnimationState(AnimatorPlayerController.States.Attack);
    }

    public void Die()
    {
        ChangeAnimationState(AnimatorPlayerController.States.Die);
    }

    public void Run()
    {
        ChangeAnimationState(AnimatorPlayerController.States.Run);
    }

    public void Hurt()
    {
        ChangeAnimationState(AnimatorPlayerController.States.Hurt);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void ChangeAnimationState(string newAnimation)
    {
        if (_currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);
        _currentAnimation = newAnimation;
    }
}
