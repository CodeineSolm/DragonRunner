using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FireballAnimations : MonoBehaviour
{
    private Animator _animator;

    public void Explosion()
    {
        _animator.SetTrigger(AnimatorFireballController.States.Explosion);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
