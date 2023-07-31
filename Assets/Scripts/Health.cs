using System;

public class Health : IReadonlyHealth
{
    private int _maxHealth;
    private int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsAlive => _currentHealth > 0;

    public event Action<int> HealthChanged;
    public event Action Dead;

    public Health(int maxHealth)
    {
        Reset(maxHealth);
    }

    public void Reset(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        if (IsAlive)
        {
            _currentHealth -= damage;

            if (IsAlive == false)
            {
                _currentHealth = 0;
                Dead?.Invoke();
            }
            HealthChanged?.Invoke(_currentHealth);
        }
    }
}
