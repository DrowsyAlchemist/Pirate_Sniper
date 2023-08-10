using System;
using System.Collections;

public class Enemy
{
    public readonly EnemyPreset Preset;
    private readonly Health _health;
    private readonly EnemyAnimator _animator;

    public Player Target { get; private set; }
    public IReadonlyHealth ReadonlyHealth => _health;
    public EnemyAnimator Animator => _animator;

    public event Action Dead;
    public event Action<int> Headshot;

    public Enemy(EnemyPreset preset, Player player, EnemyAnimator enemyAnimator)
    {
        _health = new(preset.Health);
        Preset = preset;
        Target = player;
        _animator = enemyAnimator;
        enemyAnimator.DeathFinished += OnDead;
    }

    ~Enemy()
    {
        _animator.DeathFinished -= OnDead;
    }

    public virtual void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        _health.TakeDamage(damage);
    }

    public int ApplyHeadshot(int damage)
    {
        int increasedDamage = (int)(Preset.HeadshotDamageModifier * damage);
        Headshot?.Invoke(increasedDamage);
        ApplyDamage(increasedDamage);
        return increasedDamage;
    }

    private void OnDead()
    {
        Dead?.Invoke();
    }
}
