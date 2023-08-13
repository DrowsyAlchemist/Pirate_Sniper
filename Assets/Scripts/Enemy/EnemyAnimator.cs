using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string IdleAnimation = "Idle";
    private const string WalkAnimation = "Walk";
    private const string ReactAnimation = "React";
    private const string ShootAnimation = "Shoot";
    private const string DeathAnimation = "Death";

    public event Action Shooted;
    public event Action DeathFinished;

    public void PlayWalk()
    {
       throw new NotImplementedException();
    }

    public void PlayReact()
    {
        _animator.SetTrigger(ReactAnimation);
    }

    public void PlayShoot()
    {
        _animator.SetTrigger(ShootAnimation);
    }

    public void PlayDeath()
    {
        _animator.ResetTrigger(ReactAnimation);
        _animator.ResetTrigger(ShootAnimation);
        _animator.SetTrigger(DeathAnimation);
    }

    private void OnDeathAnimationFinished()
    {
        DeathFinished?.Invoke();
    }

    private void OnAnimatorShooted()
    {
        Shooted?.Invoke();
    }
}
