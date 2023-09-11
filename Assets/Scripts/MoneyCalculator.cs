public static class MoneyCalculator
{
    public static int Calculate(LevelObserver levelObserver)
    {
        int moneyForEnemies = Settings.Money.MoneyForEachEnemy * levelObserver.LevelInstance.Enemies.Count;
        int moneyForHeadshots = Settings.Money.MoneyForHeadshot * levelObserver.HeadShots;
        int result = moneyForEnemies + moneyForHeadshots;

        if ((levelObserver.Score > levelObserver.LevelInstance.Score))
            return (int)(result * CalculateModifier(levelObserver));
        else
            return moneyForHeadshots;
    }

    private static float CalculateModifier(LevelObserver levelObserver)
    {
        if (levelObserver.LevelInstance.Score > 0)
            return 1 - (float)levelObserver.LevelInstance.Score / levelObserver.Score;
        else
            return 1;
    }
}