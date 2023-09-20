using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Characteristic", menuName = "Characteristic", order = 51)]
public class Characteristic : ScriptableObject
{
    [SerializeField] private CharacteristicLevel[] _levels;

    public int DefaultValue => _levels[0].Value;

    public int GetCurrentLevelIndex(int value)
    {
        for (int i = 0; i < _levels.Length; i++)
            if (_levels[i].Value == value)
                return i;

        throw new NotImplementedException();
    }

    public CharacteristicLevel GetCurrentLevel(int value)
    {
        return _levels[GetCurrentLevelIndex(value)];
    }

    public bool HasNextLevel(int value)
    {
        return GetCurrentLevelIndex(value) + 1 < _levels.Length;
    }

    public CharacteristicLevel GetNextLevel(int value)
    {
        int index = GetCurrentLevelIndex(value) + 1;

        if (index >= _levels.Length)
            throw new InvalidOperationException();

        return _levels[index];
    }
}
