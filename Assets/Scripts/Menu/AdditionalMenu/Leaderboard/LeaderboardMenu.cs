using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenu : Window
{
    [SerializeField] private RectTransform _authorizePanel;
    [SerializeField] private UIButton _authorizeButton;

    [SerializeField] private LeaderboardEntryRenderer _entryRendererTemplate;
    [SerializeField] private LeaderboardEntryRenderer _playerEntryRenderer;
    [SerializeField] private RectTransform _container;

    private Saver _saver;
    private List<LeaderboardEntryRenderer> _entryRenderers = new();

    public void Init(Saver saver)
    {
        _saver = saver;
        _authorizeButton.AddOnClickAction(OnAuthorizationButtonClick);
    }

    public override void Open()
    {
        base.Open();
#if UNITY_EDITOR
        RenderPlayerOnly();
        return;
#endif
        if (PlayerAccount.IsAuthorized)
            RenderLeaders();
        else
            RenderPlayerOnly();
    }

    private void OnAuthorizationButtonClick()
    {
        PlayerAccount.Authorize(
            onSuccessCallback: OnAuthorized,
            onErrorCallback: (error) => Debug.Log("Authorization error: " + error));
    }

    private void OnAuthorized()
    {
        if (PlayerAccount.HasPersonalProfileDataPermission)
        {
            RenderLeaders();
        }
        else
        {
            PlayerAccount.RequestPersonalProfileDataPermission(
                onSuccessCallback: RenderLeaders,
                onErrorCallback: (error) =>
                {
                    Debug.Log("RequestPersonalProfileDataPermission error: " + error);
                    RenderLeaders();
                });
        }
    }

    private void RenderLeaders()
    {
        _authorizePanel.Deactivate();
        Leaderboard.GetPlayerEntry(Settings.Leaderboard.LeaderboardName,
                onSuccessCallback: (result) => _playerEntryRenderer.Render(result));
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

    private void RenderPlayerOnly()
    {
        _authorizePanel.Activate();
        _playerEntryRenderer.Render("?", _saver.GetScore().ToString(), Settings.Leaderboard.DefaultName);
    }
}