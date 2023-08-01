using System.Collections.Generic;
using UnityEngine;

public class LevelPreset : MonoBehaviour
{
    [SerializeField] private EnemyBody[] _enemies;

    public bool IsCompleted => Score > 0;
    public Location Location => LocationsStorage.GetLocation(this);
    public int IndexInLocation => LocationsStorage.GetLocation(this).GetLevelIndex(this);
    public int Stars => Settings.Score.GetStars(Score);
    public int Score => Level.GetLevelScore(this);

    public IReadOnlyCollection<EnemyBody> Enemies => _enemies;
}
