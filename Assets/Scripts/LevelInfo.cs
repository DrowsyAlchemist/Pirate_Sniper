using System;

public class LevelInfo
{
    private readonly Level _levelInstance;

    public int _score;
    public int _stars;
    public float _time;

    public Level LevelInstance => _levelInstance;
    public int EnemiesLeft { get; private set; }
    public int Score => _score;
    public int Stars => _stars;
    public float Time => _time;
    public event Action Completed;

    public LevelInfo(Level levelInstance)
    {
        _levelInstance = levelInstance;

        foreach (var enemyBody in levelInstance.Enemies)
        {
            enemyBody.Init();
            enemyBody.Enemy.ReadonlyHealth.Dead += OnEnemyDead;
        }
        EnemiesLeft = levelInstance.Enemies.Count;
    }

    private void OnEnemyDead()
    {
        EnemiesLeft--;

        if (EnemiesLeft == 0)
            Completed?.Invoke();
    }
}