using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private EnemyBody[] _enemies;

    public int EnemiesCount => _enemies.Length;
    public int EnemiesLeft { get; private set; }
    public bool IsCompleted { get; private set; }
    public Location Location => LocationsStorage.GetLocation(this);
    public int IndexInLocation => LocationsStorage.GetLocation(this).GetLevelIndex(this);
    public int Stars { get; private set; }
    public int Score => Game.GetLevelScore(this);

    public IEnumerable<EnemyBody> Enemies => _enemies;

    public event Action Completed;

    public void Init()
    {
        foreach (var enemyBody in _enemies)
        {
            enemyBody.Init();
            enemyBody.Enemy.ReadonlyHealth.Dead += OnEnemyDead;
        }
        EnemiesLeft = _enemies.Length;
    }

    private void OnDestroy()
    {
        foreach (var enemyBody in _enemies)
            enemyBody.Enemy.ReadonlyHealth.Dead -= OnEnemyDead;
    }

    private void OnEnemyDead()
    {
        EnemiesLeft--;

        if (EnemiesLeft == 0)
            Completed?.Invoke();
    }
}
