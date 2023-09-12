using UnityEngine;

public class ShootingState : EnemyState
{
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private ParticleSystem _shotEffect;

    private Timer _timer;

    public float SecondsBetweenShots => Random.Range(Enemy.Preset.MinSecondsBetweenShots, Enemy.Preset.MaxSecondsBetweenShots);

    private void Start()
    {
        Enemy.Animator.Shooted += OnAnimatorShoot;
    }

    private void OnEnable()
    {
        if (_timer == null)
            Init();

        _timer.Reset();
        Enemy.Animator.PlayReact();
        Enemy.Animator.PlayShoot();
    }

    private void OnDestroy()
    {
        if (_timer != null)
            _timer.WentOff -= OnTimerWentOff;

        Enemy.Animator.Shooted -= OnAnimatorShoot;
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

        if (Enemy.ReadonlyHealth.IsAlive)
            Enemy.Animator.PlayShoot();
    }

    private void OnAnimatorShoot()
    {
        if (Enemy.ReadonlyHealth.IsAlive)
        {
            _shotEffect.Play();
            _shootSound.Play();
            _timer.Start(SecondsBetweenShots);

            if (Random.Range(0, 100) < Enemy.Preset.AccuracyInPercents)
                Player.ApplyDamage(Enemy.Preset.Damage);
        }
    }
}
