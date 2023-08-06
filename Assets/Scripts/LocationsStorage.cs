using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationsStorage : MonoBehaviour
{
    [SerializeField] private Location[] _locations;

    private static LocationsStorage _instance;

    public static IReadOnlyCollection<Location> Locations => _instance._locations;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static int GetIndex(Location location)
    {
        for (int i = 0; i < _instance._locations.Length; i++)
            if (_instance._locations[i] == location)
                return i;

        throw new InvalidOperationException("There are no such location in the storage");
    }

    public static Location GetLocation(LevelPreset level)
    {
        return _instance._locations[GetLocationIndex(level)];
    }

    public static int GetLocationIndex(LevelPreset level)
    {
        for (int i = 0; i < _instance._locations.Length; i++)
            foreach (var lvl in _instance._locations[i].Levels)
                if (lvl.Equals(level))
                    return i;

        throw new InvalidOperationException("There are no such level in the storage");
    }

    public static Location GetLocationByIndex(int index)
    {
        if (index < 0 || index >= _instance._locations.Length)
            throw new ArgumentOutOfRangeException("index");

        return _instance._locations[index];
    }
}
