using System.Collections.Generic;
using UnityEngine;

public class LevelPreset : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private EnemyBody[] _enemies;

    public string Id => _id;
    public bool IsCompleted => Score > 0;
    public Location Location => LocationsStorage.GetLocation(this);
    public int IndexInLocation => LocationsStorage.GetLocation(this).GetLevelIndex(this);
    public int Stars => Settings.Score.GetStars(Score);
    public int Score => Level.GetLevelScore(this);

    public IReadOnlyCollection<EnemyBody> Enemies => _enemies;

    public override bool Equals(object other)
    {
        if (other is LevelPreset levelPreset)
            return _id.Equals(levelPreset.Id);
        else
            return false;
    }
}
