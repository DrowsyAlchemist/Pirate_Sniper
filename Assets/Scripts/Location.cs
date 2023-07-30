using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Location", order = 51)]
public class Location : ScriptableObject
{
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _requiredStars;
    [SerializeField] private Level[] _levels;

    public string Lable => _lable;
    public Sprite Sprite => _sprite;
    public IReadOnlyList<Level> Levels => _levels;
    public int RequiredStars => _requiredStars;
    public int Index => LocationsStorage.GetIndex(this);

    public bool IsCompleted
    {
        get
        {
            foreach (var level in _levels)
                if (level.IsCompleted == false)
                    return false;

            return true;
        }
    }

    public int Stars
    {
        get
        {
            int stars = 0;

            foreach (var level in _levels)
                stars += level.Stars;

            return stars;
        }
    }

    public int GetLevelIndex(Level level)
    {
        for (int i = 0; i < _levels.Length; i++)
            if (_levels[i] == level)
                return i;

        throw new InvalidOperationException("There are no such level in the location");
    }

    public Level GetLevelByIndex(int index)
    {
        if (index < 0 || index >= _levels.Length)
            throw new ArgumentOutOfRangeException("index");

        return _levels[index];
    }
}
