using System;

public class Enemy : Creature
{
    private readonly EnemyPreset _preset;

    public event Action<int> Headshot;

    public Enemy(EnemyPreset preset) : base(preset.Health)
    {
        _preset = preset;
        Health.Dead += OnDead;
    }

    public void ApplyHeadshot(int damage)
    {
        int increasedDamage = (int)(_preset.HeadshotDamageModifier * damage);
        Headshot?.Invoke(increasedDamage);
        base.ApplyDamage(increasedDamage);
    }

    private void OnDead()
    {

    }
}
