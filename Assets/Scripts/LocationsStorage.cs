using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationsStorage : MonoBehaviour
{
    [SerializeField] private Location[] _locations;

    public IReadOnlyCollection<Location> Locations => _locations;

    public int GetLocationIndex(Location location)
    {
        for (int i = 0; i < _locations.Length; i++)
            if (_locations[i] == location)
                return i;

        throw new InvalidOperationException("There are no such location in the storage");
    }

    public Location GetLocation(LevelPreset level)
    {
        return _locations[GetLocationIndex(level)];
    }

    public int GetLocationIndex(LevelPreset level)
    {
        for (int i = 0; i < _locations.Length; i++)
            foreach (var lvl in _locations[i].Levels)
                if (lvl.Equals(level))
                    return i;

        throw new InvalidOperationException("There are no such level in the storage");
    }

    public Location GetLocationByIndex(int index)
    {
        if (index < 0 || index >= _locations.Length)
            throw new ArgumentOutOfRangeException("index");

        return _locations[index];
    }

    public int GetIndexInLocation(LevelPreset level)
    {
        var location = GetLocation(level);
        return location.GetLevelIndex(level);
    }

    public LevelPreset GetNextLevel(LevelPreset level)
    {
        var location = GetLocation(level);
        int levelIndex = GetIndexInLocation(level);

        if (levelIndex == location.Levels.Count - 1)
        {
            var nextLocation = GetNextLocation(location);

            if (nextLocation != null)
                return nextLocation.Levels[0];

            return null;
        }
        return location.Levels[levelIndex + 1];
    }

    public Location GetNextLocation(Location location)
    {
        int locationIndex = GetLocationIndex(location);

        if (locationIndex + 1 >= Locations.Count)
            return null;

        return GetLocationByIndex(locationIndex + 1);
    }

    public LevelPreset GetPreviousLevel(LevelPreset level)
    {
        var location = GetLocation(level);
        int locationIndex = GetLocationIndex(location);
        int indexInLocation = location.GetLevelIndex(level);

        if (indexInLocation == 0)
        {
            if (locationIndex == 0)
                return null;
            else
                return _locations[locationIndex - 1].Levels[^1];
        }
        return location.Levels[indexInLocation - 1];
    }

    public Location GetPreviousLocation(Location location)
    {
        int locationIndex = GetLocationIndex(location);

        if (locationIndex == 0)
            return null;

        return GetLocationByIndex(locationIndex - 1);
    }
}
