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
        _rankText.text = entry.rank.ToString();
        _scoreText.text = entry.score.ToString();

        if (PlayerAccount.HasPersonalProfileDataPermission)
            _nameText.text = entry.player.publicName;
        else
            _nameText.text = Settings.Leaderboard.DefaultName;
    }
}
