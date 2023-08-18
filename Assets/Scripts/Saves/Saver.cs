using Agava.YandexGames;
using System;
using System.Text;
using UnityEngine;

public class Saver
{
    private const string SavesName = "Saves";
    private const int DefaultScore = 0;
    private const string Devider = "_";
    private const int MaxLevelsCount = 10;

    private readonly StringBuilder _stringBuilder;
    private SaveData _saves;

    public int PlayerMoney => _saves.PlayerMoney;
    public int PlayerHealth => _saves.PlayerMaxHealth;
    public int PlayerDamage => _saves.PlayerDamage;

    public Saver()
    {
        _stringBuilder = new();
        LoadSaves(out _saves);
    }

    public void RemoveSaves()
    {
        _saves = new(_stringBuilder);
        Save();
    }

    public void SetPlayerMoney(int value)
    {
        _saves.PlayerMoney = (value >= 0) ? value : throw new ArgumentOutOfRangeException();
        Save();
    }

    public void SetPlayerHealth(int value)
    {
        _saves.PlayerMaxHealth = (value > 0) ? value : throw new ArgumentOutOfRangeException();
        Save();
    }

    public void SetPlayerDamage(int value)
    {
        _saves.PlayerDamage = (value > 0) ? value : throw new ArgumentOutOfRangeException();
        Save();
    }

    public void SaveWeaponAccuired(Weapon weapon)
    {
        _saves.Weapons += weapon.Id;
        Save();
    }

    public void SetCurrentWeapon(Weapon weapon)
    {
        _saves.CurrentWeapon = weapon.Id;
        Save();
    }

    public string GetCurrentWeapon()
    {
        return _saves.CurrentWeapon;
    }

    public bool GetWeaponAccuired(Weapon weapon)
    {
        return _saves.Weapons.Contains(weapon.Id);
    }

    public int GetLevelScore(int locationIndex, int levelIndex)
    {
        switch (locationIndex)
        {
            case 0:
                if (int.TryParse(_saves.ZeroLocation.Split(Devider)[levelIndex], out int score))
                    return score;
                else
                    throw new Exception("GetLevelScore fail");
            default:
                throw new NotImplementedException();
        }
    }

    public int GetLevelScore(LevelPreset level)
    {
        Location location = LocationsStorage.GetLocation(level);
        return GetLevelScore(LocationsStorage.GetLocationIndex(level), location.GetLevelIndex(level));
    }

    public int GetScore()
    {
        int zeroLocationScore = 0;
        int firstLocationScore = 0;
        string[] zeroLocationsLevels = _saves.ZeroLocation.Split(Devider);
        string[] firstLocationsLevels = _saves.ZeroLocation.Split(Devider);

        for (int i = 0; i < MaxLevelsCount; i++)
        {
            if (int.TryParse(zeroLocationsLevels[i], out int levelScoreInt))
                zeroLocationScore += levelScoreInt;
            else
                Debug.Log($"ParsingError: value was {zeroLocationsLevels[i]}, i was {i}");
        }
        for (int i = 0; i < MaxLevelsCount; i++)
        {
            if (int.TryParse(firstLocationsLevels[i], out int levelScoreInt))
                firstLocationScore += levelScoreInt;
            else
                Debug.Log($"ParsingError: value was {firstLocationsLevels[i]}, i was {i}");
        }
        return zeroLocationScore + firstLocationScore;
    }

    public void SaveLevel(LevelPreset level, int score)
    {
        SaveLevel(level.Location.Index, level.IndexInLocation, score);
        Save();
#if UNITY_EDITOR
        Debug.Log("GENERAL_SCORE: " + GetScore());
        return;
#endif
        if (PlayerAccount.IsAuthorized)
            Leaderboard.SetScore(Settings.Leaderboard.LeaderboardName, GetScore());
    }

    public void SaveLevel(int locationNumber, int levelIndex, int score)
    {
        switch (locationNumber)
        {
            case 0:
                _saves.ZeroLocation = ReplaceScore(_saves.ZeroLocation, levelIndex, score);
                break;
            default:
                throw new NotImplementedException();
        }
        Save();
    }

    private void Save()
    {
#if UNITY_EDITOR
        PlayerPrefs.SetString(SavesName, JsonUtility.ToJson(_saves));
        return;
#endif
        PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(_saves));
    }

    private void LoadSaves(out SaveData saves)
    {
        string jsonData = null;
#if UNITY_EDITOR
        jsonData = PlayerPrefs.GetString(SavesName);
        SetSaves(out saves, jsonData);
        return;
#endif
        PlayerAccount.GetCloudSaveData((result) => jsonData = result);
        SetSaves(out saves, jsonData);
    }

    private void SetSaves(out SaveData saves, string jsonData)
    {
        saves = JsonUtility.FromJson<SaveData>(jsonData) ?? new(_stringBuilder);
    }

    private string ReplaceScore(string locationString, int levelIndex, int score)
    {
        string[] locationLevelsScores = SplitLocationString(locationString);
        locationLevelsScores[levelIndex] = score.ToString();
        return BuildLocationString(locationLevelsScores);
    }

    private string BuildLocationString(string[] levelScores)
    {
        _stringBuilder.Clear();

        foreach (string str in levelScores)
            _stringBuilder.Append(str + Devider);

        return _stringBuilder.ToString();
    }

    private string[] SplitLocationString(string locationString)
    {
        return locationString.Split(Devider);
    }

    [Serializable]
    private class SaveData
    {
        public int PlayerMoney;
        public int PlayerMaxHealth;
        public int PlayerDamage;

        public string ZeroLocation;
        public string FirstLocation;

        public string CurrentWeapon;
        public string Weapons;

        public SaveData(StringBuilder stringBuilder)
        {
            PlayerMaxHealth = Settings.Characteristics.Health.DefaultValue;
            PlayerDamage = Settings.Characteristics.Damage.DefaultValue;

            stringBuilder.Clear();

            for (int i = 0; i < MaxLevelsCount; i++)
                stringBuilder.Append(DefaultScore.ToString() + Devider);

            string defaultString = stringBuilder.ToString();
            ZeroLocation = defaultString;
            FirstLocation = defaultString;
            CurrentWeapon = Settings.Shooting.DefaultWeapon.Id;
            Weapons = CurrentWeapon;
        }
    }
}
