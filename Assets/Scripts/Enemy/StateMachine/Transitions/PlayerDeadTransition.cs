public class PlayerDeadTransition : EnemyTransition
{
    private void Start()
    {
        Player.Health.Dead += OnPlayerDead;
    }

    private void OnDestroy()
    {
        Player.Health.Dead -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        NeedTransit = true;
    }
}
