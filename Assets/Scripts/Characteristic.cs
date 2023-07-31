using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Characteristic", menuName = "Characteristic", order = 51)]
public class Characteristic : ScriptableObject
{
    [SerializeField] private Level[] _levels;

    public int DefaultValue => _levels[0].Value;

    public int GetCurrentLevelIndex(int value)
    {
        for (int i = 0; i < _levels.Length; i++)
            if (_levels[i].Value == value)
                return i;

        throw new NotImplementedException();
    }

    public Level GetCurrentLevel(int value)
    {
        return _levels[GetCurrentLevelIndex(value)];
    }

    public bool HasNextLevel(int value)
    {
        return GetCurrentLevelIndex(value) + 1 < _levels.Length;
    }

    public Level GetNextLevel(int value)
    {
        int index = GetCurrentLevelIndex(value) + 1;

        if (index >= _levels.Length)
            throw new InvalidOperationException();

        return _levels[index];
    }

    [Serializable]
    public class Level
    {
        [SerializeField] private int _value;
        [SerializeField] private int _cost;

        public int Value => _value;
        public int Cost => _cost;
    }
}
