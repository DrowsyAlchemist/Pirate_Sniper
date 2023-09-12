using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenu : Window
{
    [SerializeField] private LeaderboardEntryRenderer _entryRendererTemplate;
    [SerializeField] private LeaderboardEntryRenderer _playerEntryRenderer;
    [SerializeField] private RectTransform _container;

    private Saver _saver;
    private bool _isPlayerAuthorized;
    private List<LeaderboardEntryRenderer> _entryRenderers = new();

    public void Init(Saver saver)
    {
        _saver = saver;
    }

    public override void Open()
    {
        base.Open();
#if UNITY_EDITOR
        _playerEntryRenderer.Render("?", _saver.GetScore().ToString(), Settings.Leaderboard.DefaultName);
        return;
#endif
        RenderLeaders();
    }

    public void RenderLeaders()
    {
        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.GetPlayerEntry(Settings.Leaderboard.LeaderboardName,
                onSuccessCallback: (result) => _playerEntryRenderer.Render(result));
        }
        else
        {
            _playerEntryRenderer.Render("?", _saver.GetScore().ToString(), Settings.Leaderboard.DefaultName);
        }
        Leaderboard.GetEntries(Settings.Leaderboard.LeaderboardName,
            onSuccessCallback: (result) =>
            {
                int i = 0;

                foreach (var entry in result.entries)
                {
                    if (_entryRenderers.Count < i + 1)
                    {
                        var entryRenderer = Instantiate(_entryRendererTemplate, _container);
                        _entryRenderers.Add(entryRenderer);
                    }
                    _entryRenderers[i].Render(entry);
                    i++;
                }
            },
            onErrorCallback: (error) => Debug.Log("Leaderboard error: " + error),
            topPlayersCount: Settings.Leaderboard.TopPlayersCount,
            competingPlayersCount: Settings.Leaderboard.CompetingPlayersCount,
            includeSelf: Settings.Leaderboard.IncludeSelf,
            pictureSize: Settings.Leaderboard.ProfilePictureSize
        );
    }
}