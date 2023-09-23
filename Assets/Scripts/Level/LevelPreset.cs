using System.Collections.Generic;
using UnityEngine;

public class LevelPreset : MonoBehaviour
{
    [SerializeField] private string _id;
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private EnemyBody[] _enemies;

    private Level _level;

    public Transform CameraTransform => _cameraPosition;
    public int Stars => Settings.Score.GetStars(Score);
    public int Score => _level.GetLevelScore(this);

    public IReadOnlyCollection<EnemyBody> Enemies => _enemies;

    public void Init(Level level)
    {
        _level = level;
    }

    public override bool Equals(object other)
    {
        if (other is LevelPreset levelPreset)
            return _id.Equals(levelPreset._id);
        else
            return false;
    }
}
