using System;
using UnityEngine;

public class EnemyBody : MonoBehaviour,IApplyDamage
{
    [SerializeField] private EnemyPreset _preset;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private EnemyHead _head;

    private Enemy _enemy;

    public Enemy Enemy => _enemy;

    public void Init()
    {
        _enemy = new Enemy(_preset);
        _enemy.ReadonlyHealth.Dead += OnDead;
        _head.Damaged += OnHeadshot;
    }

    private void OnDestroy()
    {
        _enemy.ReadonlyHealth.Dead -= OnDead;
        _head.Damaged -= OnHeadshot;
    }

    public void ApplyDamage(int damage)
    {
        Enemy.ApplyDamage(damage);
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }

    private void OnHeadshot(int damage)
    {
        _enemy.ApplyHeadshot(damage);
    }
}
