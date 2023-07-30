using System;
using UnityEngine;

public class Player : Creature
{
    private int _initialDamage;

    private InputController _inputController;

    public event Action Shooted;

    public Player(InputController inputController, int maxHealth, int damage) : base(maxHealth)
    {
        _inputController = inputController;
        inputController.Shooted += OnShooted;
        _initialDamage = damage;
    }

    ~Player()
    {
        _inputController.Shooted -= OnShooted;
    }

    private void OnShooted(RaycastHit hit)
    {
        Shooted?.Invoke();

        if (hit.collider.TryGetComponent(out IApplyDamage target))
            target.ApplyDamage(_initialDamage);
    }
}
