using System;

public abstract class Creature : IApplyDamage
{
    protected readonly Health Health;

    public IReadonlyHealth ReadonlyHealth => Health;

    public Creature(int maxHealth)
    {
        Health = new Health(maxHealth);
    }

    public virtual void ApplyDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException();

        Health.TakeDamage(damage);
    }
}