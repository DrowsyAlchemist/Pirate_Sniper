using System;

public interface IReadonlyHealth
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; }
    public bool IsAlive { get; }

    public event Action<int> HealthChanged;
    public event Action Dead;
}
