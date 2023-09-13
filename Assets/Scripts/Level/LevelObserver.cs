using System;

public class LevelObserver
{
    private LevelPreset _levelInstance;
    private readonly Stopwatch _stopwatch;
    private readonly Player _player;

    public LevelPreset LevelInstance => _levelInstance;
    public int EnemiesCount => _levelInstance.Enemies.Count;
    public int EnemiesLeft { get; private set; }

    public int ShotsCount { get; private set; }
    public int HeadShots { get; private set; }
    public float CompleteTime { get; private set; }
    public int Score { get; private set; }
    public int Stars { get; private set; }
    public int Money { get; private set; }
    public float Accuracy => (float)EnemiesCount / ShotsCount;
    public bool IsWon { get; private set; }

    public event Action EnemyHeadshot;
    public event Action EnemyDead;
    public event Action<bool> Completed;

    public LevelObserver(Player player)
    {
        _stopwatch = new();
        _player = player;
        _player.Shooted += OnShooted;
        _player.Health.Dead += OnPlayerDead;
    }

    ~LevelObserver()
    {
        _player.Shooted -= OnShooted;
        _player.Health.Dead -= OnPlayerDead;
    }

    public void SetLevel(LevelPreset levelInstance)
    {
        Clear();
        _levelInstance = levelInstance;

        foreach (var enemyBody in levelInstance.Enemies)
        {
            enemyBody.Init(_player);
            enemyBody.Enemy.Dead += OnEnemyDead;
            enemyBody.Enemy.Headshot += OnHeadShot;
        }
        EnemiesLeft = levelInstance.Enemies.Count;
    }

    public void Start()
    {
        if (Score > 0)
            throw new InvalidOperationException("You should Set Level before start");

        _player.Shooted += StartStopwatch;
    }

    public void Clear()
    {
        if (_levelInstance != null)
        {
            foreach (var enemyBody in _levelInstance.Enemies)
            {
                enemyBody.Enemy.Dead -= OnEnemyDead;
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
        IsWon = false;
    }

    private void StartStopwatch()
    {
        _stopwatch.Start();
        _player.Shooted -= StartStopwatch;
    }

    private void Complete(bool isWon)
    {
        _stopwatch.Stop();

        if (isWon)
        {
            CompleteTime = _stopwatch.ElapsedTime;
            Score = ScoreCalculator.Calculate(this);
            Stars = Settings.Score.GetStars(Score);
            Money = MoneyCalculator.Calculate(this);
        }
        IsWon = isWon;
        Completed?.Invoke(isWon);
    }

    private void OnEnemyDead()
    {
        EnemiesLeft--;
        EnemyDead?.Invoke();

        if (EnemiesLeft == 0)
            Complete(isWon: true);
    }

    private void OnPlayerDead()
    {
        Complete(isWon: false);
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