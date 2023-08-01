using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Location", order = 51)]
public class Location : ScriptableObject
{
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _requiredStars;
    [SerializeField] private LevelPreset[] _levels;

    public string Lable => _lable;
    public Sprite Sprite => _sprite;
    public IReadOnlyList<LevelPreset> Levels => _levels;
    public int RequiredStars => _requiredStars;
    public int Index => LocationsStorage.GetIndex(this);

    public bool IsCompleted
    {
        get
        {
            foreach (var levelPreset in _levels)
                if (levelPreset.IsCompleted == false)
                    return false;

            return true;
        }
    }

    public int Stars
    {
        get
        {
            int stars = 0;

            foreach (var levelPreset in _levels)
                stars += levelPreset.Stars;

            return stars;
        }
    }

    public int GetLevelIndex(LevelPreset level)
    {
        for (int i = 0; i < _levels.Length; i++)
            if (_levels[i] == level)
                return i;

        throw new InvalidOperationException("There are no such level in the location");
    }

    public LevelPreset GetLevelByIndex(int index)
    {
        if (index < 0 || index >= _levels.Length)
            throw new ArgumentOutOfRangeException("index");

        return _levels[index];
    }
}
