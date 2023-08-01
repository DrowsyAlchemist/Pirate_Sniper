using UnityEngine;

public class ShootingState : EnemyState
{
    private Timer _timer;

    private void Start()
    {
        _timer = new Timer();
        _timer.WentOff += OnTimerWentOff;
    }

    private void OnDestroy()
    {
        _timer.WentOff -= OnTimerWentOff;
    }

    private void OnTimerWentOff()
    {
        int percent = Random.Range(0, 100);

        if (percent < Enemy.Preset.AccuracyInPercents)
            Shoot();
    }

    private void Shoot()
    {
        Player.ApplyDamage(Enemy.Preset.Damage);
    }
}
