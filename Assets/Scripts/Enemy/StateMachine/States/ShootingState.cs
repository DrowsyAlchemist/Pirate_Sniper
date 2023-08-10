using UnityEngine;

public class ShootingState : EnemyState
{
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private ParticleSystem _shotEffect;

    private Timer _timer;

    public float SecondsBetweenShots => Random.Range(Enemy.Preset.MinSecondsBetweenShots, Enemy.Preset.MaxSecondsBetweenShots);

    private void OnEnable()
    {
        if (_timer == null)
            Init();

        Enemy.Animator.PlayAim();
        _timer.Start(SecondsBetweenShots);
    }

    private void OnDisable()
    {
        _timer?.Reset();
    }

    private void OnDestroy()
    {
        if (_timer != null)
            _timer.WentOff -= OnTimerWentOff;
    }

    private void Init()
    {
        _timer = new Timer();
        _timer.WentOff += OnTimerWentOff;
    }

    private void OnTimerWentOff()
    {
        if (enabled == false)
            return;

        if (Random.Range(0, 100) < Enemy.Preset.AccuracyInPercents)
            Player.ApplyDamage(Enemy.Preset.Damage);

        _shotEffect.Play();
        _shootSound.Play();
        Enemy.Animator.PlayShoot();
        _timer.Start(SecondsBetweenShots);
    }
}
