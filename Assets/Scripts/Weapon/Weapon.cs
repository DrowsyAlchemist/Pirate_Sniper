using System;
using UnityEngine;

[RequireComponent(typeof(WeaponAnimator))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponInfo _info;
    [SerializeField] private ParticleSystem _shotEffect;

    private WeaponAnimator _animator;
    private Timer _timer;

    public WeaponInfo Info => _info;
    public bool IsReady => (_animator.IsPlaying == false) && (SecondsBeforeReadyLeft < Settings.Epsilon);
    public float SecondsBeforeReadyLeft => _info.SecondsBetweenShots - _timer.ElapsedTime;

    public void Init()
    {
        _animator = GetComponent<WeaponAnimator>();
        _shotEffect.Play();
        _timer = new Timer();
        _timer.Start(_info.SecondsBetweenShots);
    }

    public void Shoot(RaycastHit hit, int playerDamage)
    {
        if (IsReady == false)
            throw new InvalidOperationException();

        _shotEffect.Play();
        _animator.PlayShot();
        _timer.Start(_info.SecondsBetweenShots);

        if (hit.collider.TryGetComponent(out IApplyDamage target))
            target.ApplyDamage(_info.Damage + playerDamage);
    }
}
