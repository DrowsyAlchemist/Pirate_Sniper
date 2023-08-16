using System.Collections.Generic;
using UnityEngine;

public class LevelPreset : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private EnemyBody[] _enemies;

    public Transform CameraTransform => _cameraPosition;
    public bool IsCompleted => Score > 0;
    public Location Location => LocationsStorage.GetLocation(this);
    public int IndexInLocation => LocationsStorage.GetLocation(this).GetLevelIndex(this);
    public int Stars => Settings.Score.GetStars(Score);
    public int Score => Level.GetLevelScore(this);

    public IReadOnlyCollection<EnemyBody> Enemies => _enemies;

    public override bool Equals(object other)
    {
        if (other is LevelPreset levelPreset)
            return _id.Equals(levelPreset._id);
        else
            return false;
    }

    public LevelPreset GetPreviousLevel()
    {
        if (IndexInLocation == 0)
        {
            if (Location.Index == 0)
                return null;
            else
                return Location.GetPreviousLocation().Levels[^1];
        }
        return Location.Levels[IndexInLocation - 1];
    }

    public bool TryGetNextLevel(out LevelPreset nextLevel)
    {
        nextLevel = null;

        if (IndexInLocation == Location.Levels.Count - 1)
        {
            if (Location.TryGetNextLocation(out Location nextLocation))
            {
                nextLevel = nextLocation.Levels[0];
                return true;
            }
            return false;
        }
        nextLevel = Location.Levels[IndexInLocation + 1];
        return true;
    }
}
