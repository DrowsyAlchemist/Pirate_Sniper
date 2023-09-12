using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntryRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _rankText;
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _scoreText;

    public void Render(LeaderboardEntryResponse entry)
    {
        Render(entry.rank.ToString(), entry.score.ToString(), entry.player.publicName);
    }

    public void Render(string rank, string score, string name)
    {
        if (string.IsNullOrEmpty(name))
            name = Settings.Leaderboard.DefaultName;

        _rankText.text = rank;
        _scoreText.text = score;
        _nameText.text = name;
    }
}
