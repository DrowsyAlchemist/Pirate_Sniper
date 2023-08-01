public class DeadTransition : EnemyTransition
{
    private void Start()
    {
        Enemy.ReadonlyHealth.Dead += OnDead;
    }

    private void OnDestroy()
    {
        Enemy.ReadonlyHealth.Dead -= OnDead;
    }

    private void OnDead()
    {
        NeedTransit = true;
    }
}
