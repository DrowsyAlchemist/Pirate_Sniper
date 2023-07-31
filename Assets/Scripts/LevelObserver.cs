using System;

public class LevelObserver
{
    private Level _levelInstance;
    private readonly Stopwatch _stopwatch;
    private readonly Player _player;

    public Level LevelInstance => _levelInstance;
    public int EnemiesCount => _levelInstance.Enemies.Count;
    public int EnemiesLeft { get; private set; }

    public int ShotsCount { get; private set; }
    public int HeadShots { get; private set; }
    public float CompleteTime { get; private set; }
    public int Score { get; private set; }
    public int Stars { get; private set; }
    public int Money { get; private set; }
    public float Accuracy => (float)EnemiesCount / ShotsCount;

    public event Action EnemyHeadshot;
    public event Action EnemyDead;
    public event Action Completed;

    public LevelObserver(Player player)
    {
        _stopwatch = new();
        _player = player;
        _player.Shooted += OnShooted;
    }

    ~LevelObserver()
    {
        _player.Shooted -= OnShooted;
    }

    public void SetLevel(Level levelInstance)
    {
        Clear();
        _levelInstance = levelInstance;

        foreach (var enemyBody in levelInstance.Enemies)
        {
            enemyBody.Init();
            enemyBody.Enemy.ReadonlyHealth.Dead += OnEnemyDead;
            enemyBody.Enemy.Headshot += OnHeadShot;
        }
        EnemiesLeft = levelInstance.Enemies.Count;
    }

    public void Start()
    {
        if (Score > 0)
            throw new InvalidOperationException("You should clear before start");

        _stopwatch.Reset();
        _stopwatch.Start();
    }

    private void Stop()
    {
        _stopwatch.Stop();
        CompleteTime = _stopwatch.ElapsedTime;
        Score = ScoreCalculator.Calculate(this);
        Stars = Settings.Score.GetStars(Score);
        Money = MoneyCalculator.Calculate(this);
        Completed?.Invoke();
    }

    private void Clear()
    {
        if (_levelInstance != null)
        {
            foreach (var enemyBody in _levelInstance.Enemies)
            {
                enemyBody.Enemy.ReadonlyHealth.Dead -= OnEnemyDead;
                enemyBody.Enemy.Headshot -= OnHeadShot;
            }
        }
        _levelInstance = null;
        ShotsCount = 0;
        HeadShots = 0;
        CompleteTime = 0;
        Stars = 0;
        Score = 0;
        Money = 0;
    }

    private void OnEnemyDead()
    {
        EnemiesLeft--;
        EnemyDead?.Invoke();

        if (EnemiesLeft == 0)
            Stop();
    }

    private void OnShooted()
    {
        ShotsCount++;
    }


    private void OnHeadShot(int _)
    {
        HeadShots++;
        EnemyHeadshot?.Invoke();
    }
}