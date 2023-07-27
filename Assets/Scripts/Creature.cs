public abstract class Creature
{
    protected readonly Health Health;

    public IReadonlyHealth ReadonlyHealth => Health;

    public Creature(int maxHealth)
    {
        Health = new Health(maxHealth);
    }
}