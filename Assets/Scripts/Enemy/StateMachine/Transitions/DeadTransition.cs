public class DeadTransition : EnemyTransition
{
    private void Update()
    {
        if (Enemy.ReadonlyHealth.IsAlive == false)
            NeedTransit = true;
    }
}
