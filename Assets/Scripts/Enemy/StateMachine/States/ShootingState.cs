using UnityEngine;

public class ShootingState : EnemyState
{
    [SerializeField] private ParticleSystem _shotEffect;

    private Timer _timer;

    private void OnEnable()
    {
        if (_timer == null)
            Init();

        _timer.Start(Enemy.Preset.SecondsBetweenShots);
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
        if (Random.Range(0, 100) < Enemy.Preset.AccuracyInPercents)
            Player.ApplyDamage(Enemy.Preset.Damage);

        _shotEffect.Play();
        _timer.Start(Enemy.Preset.SecondsBetweenShots);
    }
}
