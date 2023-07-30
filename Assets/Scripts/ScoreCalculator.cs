using System;

public static class ScoreCalculator
{
    public static int Calculate(LevelInfo levelInfo)
    {
        float completeTimeScore = CalculateCompleteTimeScore(levelInfo.CompleteTime, levelInfo.EnemiesCount);
        float accuracyScore = CalculateAccuracyScore(levelInfo.Accuracy);
        float headshotsScore = CalculateHeadshotsScore(levelInfo.HeadShots, levelInfo.ShotsCount);
        return (int)(completeTimeScore + accuracyScore + headshotsScore);
    }

    private static float CalculateCompleteTimeScore(float completeTime, int enemiesCount)
    {
        float completeTimeNormalizedScore = Math.Clamp((Settings.Score.MinTimeForEachEnemy * enemiesCount) / completeTime, 0, 1);
        return Settings.Score.CompleteTimeModifier * completeTimeNormalizedScore;
    }

    private static float CalculateAccuracyScore(float accuracy)
    {
        return Settings.Score.AccuracyModifier * accuracy;
    }

    private static float CalculateHeadshotsScore(int headshots, int shots)
    {
        return Settings.Score.HeadshotsModifier * ((float)headshots / shots);
    }
}
