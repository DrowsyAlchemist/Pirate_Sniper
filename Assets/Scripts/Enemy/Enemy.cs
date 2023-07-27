using System;
using UnityEngine;

public class Enemy : Creature
{
    private EnemySettings _settings;

    public Enemy(int maxHealth) : base(maxHealth)
    {
        Health.Dead += OnDead;
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        Health.TakeDamage(damage);
    }

    private void OnDead()
    {
        
    }
}
