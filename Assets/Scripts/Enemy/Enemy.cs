using System;

public class Enemy
{
    public readonly EnemyPreset Preset;
    private readonly Health _health;

    public Player Target { get; private set; }
    public IReadonlyHealth ReadonlyHealth => _health;

    public event Action<int> Headshot;

    public Enemy(EnemyPreset preset, Player player)
    {
        _health = new(preset.Health);
        Preset = preset;
        Target = player;
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
}
