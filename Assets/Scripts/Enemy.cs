using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemySettings _settings;
    [SerializeField] private Animator _animator;

    private Health _health;

    public event Action Dead;

    private void Init()
    {
        _health = new Health(_settings.MaxHealth);
        _health.Dead += OnDead;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        _health.TakeDamage(damage);
    }

    private void OnDead()
    {
        Dead?.Invoke();
    }
}
