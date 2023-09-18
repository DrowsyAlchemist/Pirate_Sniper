using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

[CreateAssetMenu(fileName = "LeaderboardSettings", menuName = "Settings/Leaderboard", order = 51)]
public class LeaderboardSettings : ScriptableObject
{
    [SerializeField] private string _leaderboardName = "PiratesLeaderboard";
    [SerializeField] private int _topPlayersCount = 5;
    [SerializeField] private int _competingPlayersCount = 5;
    [SerializeField] private bool _includeSelf = true;
    [SerializeField] private ProfilePictureSize _profilePictureSize = ProfilePictureSize.medium;

    [SerializeField] private string _defaultNamePhrase = "DefaultName";
    [SerializeField] private Sprite _defaultAvatar;

    public string LeaderboardName => _leaderboardName;
    public int TopPlayersCount => _topPlayersCount;
    public int CompetingPlayersCount => _competingPlayersCount;
    public bool IncludeSelf => _includeSelf;
    public ProfilePictureSize ProfilePictureSize => _profilePictureSize;
    public string DefaultName => LeanLocalization.GetTranslationText(_defaultNamePhrase);
    public Sprite DefaultAvatar => _defaultAvatar;
}
