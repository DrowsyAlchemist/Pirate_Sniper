using System;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] private EnemyPreset _preset;
    [SerializeField] private CharacterAnimator _animator;

    private Enemy _enemy;

    public Enemy Enemy => _enemy;

    public event Action HeadShot;

    public void Init()
    {
        _enemy = new Enemy(_preset.Health);
        _enemy.ReadonlyHealth.Dead += OnDead;
    }

    private void OnDestroy()
    {
        _enemy.ReadonlyHealth.Dead -= OnDead;
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }
}
