using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private const string IdleAnimation = "Idle";
    private const string DeadAnimation = "Dead";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        _animator.Play(IdleAnimation);
    }

    public void PlayDead()
    {
        _animator.Play(DeadAnimation);
    }
}
