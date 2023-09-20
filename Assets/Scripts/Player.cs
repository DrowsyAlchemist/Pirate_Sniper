using System;

public class Player : IApplyDamage
{
    private readonly Saver _saver;
    private readonly Health _health;
    private readonly ShootingPoint _shootingPoint;

    public IReadonlyHealth Health => _health;
    public int MaxHealth => _saver.PlayerHealth;
    public int Damage => _saver.PlayerDamage;
    public Wallet Wallet { get; }

    public event Action Shooted;

    public Player(Saver saver, Health health, Wallet wallet, ShootingPoint shootingPoint)
    {
        _saver = saver;
        _health = health;
        Wallet = wallet;
        _shootingPoint = shootingPoint;
        _shootingPoint.Shooted += () => Shooted.Invoke();
    }

    public void Reset()
    {
        _health.Reset(_saver.PlayerHealth);
    }

    public void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        _health.TakeDamage(damage);
    }
}
