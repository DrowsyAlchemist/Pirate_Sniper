using System;

public class Enemy : Creature
{
    public readonly EnemyPreset Preset;

    public event Action<int> Headshot;

    public Enemy(EnemyPreset preset) : base(preset.Health)
    {
        Preset = preset;
        Health.Dead += OnDead;
    }

    public void ApplyHeadshot(int damage)
    {
        int increasedDamage = (int)(Preset.HeadshotDamageModifier * damage);
        Headshot?.Invoke(increasedDamage);
        base.ApplyDamage(increasedDamage);
    }

    private void OnDead()
    {

    }
}
