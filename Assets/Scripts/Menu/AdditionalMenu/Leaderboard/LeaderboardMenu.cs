using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenu : Window
{
    [SerializeField] private LeaderboardEntryRenderer _entryRendererTemplate;
    [SerializeField] private RectTransform _container;

    private bool _isPlayerAuthorized;
    private List<LeaderboardEntryRenderer> _entryRenderers = new();

    public override void Open()
    {
        base.Open();
#if UNITY_EDITOR
        return;
#endif
        RenderLeaders();
    }

    public void RenderLeaders()
    {
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