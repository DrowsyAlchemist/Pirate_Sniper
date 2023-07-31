public static class MoneyCalculator
{
    public static int Calculate(LevelObserver levelObserver)
    {
        int moneyForEnemies = Settings.Money.MoneyForEachEnemy * levelObserver.LevelInstance.Enemies.Count;
        int moneyForHeadshots = Settings.Money.MoneyForHeadshot * levelObserver.HeadShots;
        return moneyForEnemies + moneyForHeadshots;
    }
}
