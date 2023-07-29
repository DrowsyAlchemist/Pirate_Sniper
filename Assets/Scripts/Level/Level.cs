using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private EnemyBody[] _enemies;

    public int EnemiesCount => _enemies.Length;
    public bool IsCompleted { get; private set; }
    public Location Location => LocationsStorage.GetLocation(this);
    public int IndexInLocation => LocationsStorage.GetLocation(this).GetLevelIndex(this);
    public int Stars { get; private set; }
    public int Score => Game.GetLevelScore(this);

    public IReadOnlyCollection<EnemyBody> Enemies => _enemies;
}
