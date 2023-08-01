using System;

public class Enemy : Creature
{
    public readonly EnemyPreset Preset;

    public Player Target { get; private set; }

    public event Action<int> Headshot;

    public Enemy(EnemyPreset preset, Player player) : base(preset.Health)
    {
        Preset = preset;
        Health.Dead += OnDead;
        Target = player;
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
