using Agava.YandexGames;
using System;
using System.Text;
using UnityEngine;

public class Saver
{
    private const string SavesName = "Saves";
    private const int DefaultScore = 0;
    private const char Devider = ' ';
    private const int MaxLevelsCount = 10;
    private const string UnlockString = "1000 1000 1000 1000 1000 1000 1000 1000 1000 1000";

    private readonly StringBuilder _stringBuilder;
    private SaveData _saves;

    public bool IsReady { get; private set; }
    public int PlayerMoney => _saves.PlayerMoney;
    public int PlayerHealth => _saves.PlayerMaxHealth;
    public int PlayerDamage => _saves.PlayerDamage;

    public Saver()
    {
        _stringBuilder = new();
        LoadSaves();
    }

    public void RemoveSaves()
    {
        _saves = new(_stringBuilder);
        Save();
    }

    public void UnlockAllLevels()
    {
        _saves.ZeroLocation = UnlockString;
        _saves.ShipLocation0 = UnlockString;
        _saves.FirstLocation = UnlockString;
        _saves.ShipLocation1 = UnlockString;
        _saves.SecondLocation = UnlockString;
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
                return int.Parse(_saves.ZeroLocation.Split(Devider)[levelIndex]);
            case 1:
                return int.Parse(_saves.ShipLocation0.Split(Devider)[levelIndex]);
            case 2:
                return int.Parse(_saves.FirstLocation.Split(Devider)[levelIndex]);
            case 3:
                return int.Parse(_saves.ShipLocation1.Split(Devider)[levelIndex]);
            case 4:
                return int.Parse(_saves.SecondLocation.Split(Devider)[levelIndex]);
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
        int shipLocation0Score = 0;
        int firstLocationScore = 0;
        int shipLocation1Score = 0;
        int secondLocationScore = 0;

        string[] zeroLocation = _saves.ZeroLocation.Split(Devider);
        string[] shipLocation0 = _saves.ShipLocation0.Split(Devider);
        string[] firstLocation = _saves.ZeroLocation.Split(Devider);
        string[] shipLocation1 = _saves.ShipLocation1.Split(Devider);
        string[] secondLocation = _saves.SecondLocation.Split(Devider);

        for (int i = 0; i < MaxLevelsCount; i++)
            zeroLocationScore += int.Parse(zeroLocation[i]);

        for (int i = 0; i < MaxLevelsCount; i++)
            shipLocation0Score += int.Parse(shipLocation0[i]);

        for (int i = 0; i < MaxLevelsCount; i++)
            firstLocationScore += int.Parse(firstLocation[i]);

        for (int i = 0; i < MaxLevelsCount; i++)
            shipLocation1Score += int.Parse(shipLocation1[i]);

        for (int i = 0; i < MaxLevelsCount; i++)
            secondLocationScore += int.Parse(secondLocation[i]);

        return zeroLocationScore + shipLocation0Score + firstLocationScore + shipLocation1Score + secondLocationScore;
    }

    public void SaveLevel(LevelPreset level, int score)
    {
        SaveLevel(level.Location.Index, level.IndexInLocation, score);
        Save();
#if UNITY_EDITOR
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
            case 1:
                _saves.ShipLocation0 = ReplaceScore(_saves.ShipLocation0, levelIndex, score);
                break;
            case 2:
                _saves.FirstLocation = ReplaceScore(_saves.FirstLocation, levelIndex, score);
                break;
            case 3:
                _saves.ShipLocation1 = ReplaceScore(_saves.ShipLocation1, levelIndex, score);
                break;
            case 4:
                _saves.SecondLocation = ReplaceScore(_saves.SecondLocation, levelIndex, score);
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

    private void LoadSaves()
    {
#if UNITY_EDITOR
        string jsonData = PlayerPrefs.GetString(SavesName);
        SetSaves(jsonData);
        IsReady = true;
        return;
#endif
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.GetCloudSaveData(
                onSuccessCallback: (result) =>
                {
                    SetSaves(result);
                    IsReady = true;
                },
                onErrorCallback: (error) => throw new ApplicationException("Can not load CloudSaveData: " + error));
        }
        else
        {
            SetSaves(PlayerPrefs.GetString(SavesName));
            IsReady = true;
        }
    }

    private void SetSaves(string jsonData)
    {
        _saves = JsonUtility.FromJson<SaveData>(jsonData) ?? new(_stringBuilder);
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
        public string ShipLocation0;
        public string FirstLocation;
        public string ShipLocation1;
        public string SecondLocation;

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
            ShipLocation0 = defaultString;
            FirstLocation = defaultString;
            ShipLocation1 = defaultString;
            SecondLocation = defaultString;
            CurrentWeapon = Settings.Shooting.DefaultWeapon.Id;
            Weapons = CurrentWeapon;
        }
    }
}
