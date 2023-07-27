using System;
using UnityEngine;

public class Player : Creature
{
    private int _initialDamage;

    private InputController _inputController;

    public Player(InputController inputController, int maxHealth, int damage) : base(maxHealth)
    {
        _inputController = inputController;
        inputController.Shooted += DoDamage;
        _initialDamage = damage;
    }

    ~Player()
    {
        _inputController.Shooted -= DoDamage;
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        Health.TakeDamage(damage);
    }

    private void DoDamage(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out EnemyBody enemyBody))
            enemyBody.Enemy.ApplyDamage(_initialDamage);
    }
}
