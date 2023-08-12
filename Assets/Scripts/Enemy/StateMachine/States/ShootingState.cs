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

        Enemy.Animator.PlayShoot();
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

        Enemy.Animator.PlayShoot();
    }

    private void OnAnimatorShoot()
    {
        _shotEffect.Play();
        _shootSound.Play();
        _timer.Start(SecondsBetweenShots);

        if (Random.Range(0, 100) < Enemy.Preset.AccuracyInPercents)
            Player.ApplyDamage(Enemy.Preset.Damage);
    }
}
