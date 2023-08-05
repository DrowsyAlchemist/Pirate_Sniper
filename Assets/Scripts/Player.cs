using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : IApplyDamage
{
    private readonly Saver _saver;
    private readonly InputController _inputController;
    private Health _health;
    private Wallet _wallet;

    public int MaxHealth => _saver.PlayerHealth;
    public int Damage => _saver.PlayerDamage;
    public IReadonlyHealth Health => _health;
    public Wallet Wallet => _wallet;
    public Weapon Weapon { get; private set; }


    public event Action Shooted;

    public Player(InputController inputController, Saver saver)
    {
        _saver = saver;
        _health = new(MaxHealth);
        _wallet = new(saver);
        _inputController = inputController;
        inputController.Shooted += OnShooted;
    }

    ~Player()
    {
        _inputController.Shooted -= OnShooted;
    }

    public void Reset()
    {
        _health.Reset(_saver.PlayerHealth);
    }

    public void SetMaxHealth(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        _saver.SetPlayerHealth(value);
    }

    public void SetDamage(int value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException();

        _saver.SetPlayerDamage(value);
    }

    public void SetWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }

    private void OnShooted(RaycastHit hit)
    {
        if (Weapon.IsReady)
        {
            Shooted?.Invoke();
            Weapon.Shoot(hit, Damage);
        }
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        _health.TakeDamage(damage);
    }
}
