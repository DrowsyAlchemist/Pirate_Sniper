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

    private readonly StringBuilder _stringBuilder;
    private SaveData _saves;

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

    public void SetPlayerHealth(int value)
    {
        _saves.PlayerMaxHealth = (value > 0) ? value : throw new ArgumentOutOfRangeException();
    }

    public void SetPlayerDamage(int value)
    {
        _saves.PlayerDamage = (value > 0) ? value : throw new ArgumentOutOfRangeException();
    }

    public int GetLevelScore(int locationIndex, int levelIndex)
    {
        switch (locationIndex)
        {
            case 0:
                return int.Parse(_saves.ZeroLocation.Split(Devider)[levelIndex]);
            default:
                throw new NotImplementedException();
        }
    }

    public int GetLevelScore(Level level)
    {
        Location location = LocationsStorage.GetLocation(level);
        return GetLevelScore(LocationsStorage.GetLocationIndex(level), location.GetLevelIndex(level));
    }

    public void SaveLevel(Level level, int score)
    {
        SaveLevel(level.Location.Index, level.IndexInLocation, score);
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
        string jsonData;
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
        public int PlayerMaxHealth;
        public int PlayerDamage;

        public string ZeroLocation;
        public string FirstLocation;

        public SaveData(StringBuilder stringBuilder)
        {
            PlayerMaxHealth = Settings.Player.InitialHealth;
            PlayerDamage = Settings.Player.InitialDamage;

            stringBuilder.Clear();

            for (int i = 0; i < MaxLevelsCount; i++)
                stringBuilder.Append(DefaultScore.ToString() + Devider);

            string defaultString = stringBuilder.ToString();
            ZeroLocation = defaultString;
            FirstLocation = defaultString;
        }
    }
}
