using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string IdleAnimation = "Idle";
    private const string ShootAnimation = "Shoot";
    private const string AimAnimation = "Aim";
    private const string DeathAnimation = "Death";

    public event Action DeathFinished;

    public void PlayShoot()
    {
        _animator.Play(ShootAnimation);
    }

    public void PlayAim()
    {
        _animator.Play(AimAnimation);
    }

    public void PlayDeath()
    {
        _animator.SetTrigger(DeathAnimation);
    }

    private void OnDeathAnimationFinished()
    {
        DeathFinished?.Invoke();
    }
}
