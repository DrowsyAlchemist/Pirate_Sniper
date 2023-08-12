public class PlayerShootedTransition : EnemyTransition
{
    private void Start()
    {
        Player.Shooted += OnPlayerShooted;
    }

    private void OnDestroy()
    {
        Player.Shooted -= OnPlayerShooted;
    }

    private void OnPlayerShooted()
    {
        NeedTransit = true;
    }
}
