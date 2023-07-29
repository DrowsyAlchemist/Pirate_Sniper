using Agava.YandexGames;
using System;
using System.Text;
using UnityEngine;

public class SaveSystem
{
    private const string SavesName = "Saves";
    private const int DefaultScore = 0;
    private const char Devider = ' ';
    private const int MaxLevelsCount = 10;

    private readonly StringBuilder _stringBuilder;
    private SaveData _saves;

    public SaveSystem()
    {
        _stringBuilder = new();
        Load();
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

    public void Save()
    {
#if UNITY_EDITOR
        PlayerPrefs.SetString(SavesName, JsonUtility.ToJson(_saves));
        return;
#endif
        PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(_saves));
    }

    private void Load()
    {
#if UNITY_EDITOR
        _saves = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SavesName));
        _saves ??= new(_stringBuilder);
        Debug.Log(_saves.ZeroLocation);
        return;
#endif
        PlayerAccount.GetCloudSaveData((result) =>
        {
            _saves = JsonUtility.FromJson<SaveData>(result);
            _saves ??= new(_stringBuilder);
            Debug.Log(_saves.ZeroLocation);
        });
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
            stringBuilder.Clear();

            for (int i = 0; i < MaxLevelsCount; i++)
                stringBuilder.Append(DefaultScore.ToString() + Devider);

            string defaultString = stringBuilder.ToString();
            ZeroLocation = defaultString;
            FirstLocation = defaultString;
        }
    }
}
