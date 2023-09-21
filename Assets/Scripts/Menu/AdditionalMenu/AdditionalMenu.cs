using UnityEngine;

public class AdditionalMenu : AnimatedWindow
{
    [SerializeField] private LevelsMenu _levelsMenu;
    [SerializeField] private CharacteristicsMenu _characteristicsMenu;
    [SerializeField] private StoreMenu _storeMenu;
    [SerializeField] private LeaderboardMenu _leaderboardMenu;
    [SerializeField] private SettingsMenu _settingsMenu;

    private LocationMap _locationMap;

    public void Init(Player player, Saver saver, LocationMap locationMap, Sound sound)
    {
        gameObject.SetActive(true);
        _locationMap = locationMap;
        _locationMap.LocationChosen += OpenLevels;
        _characteristicsMenu.Init(saver, player.Wallet, sound.BackgroundMusic);
        _storeMenu.Init(player.Wallet, saver, sound);
        _leaderboardMenu.Init(saver);
        _settingsMenu.Init(sound);
    }

    private void OnDestroy()
    {
        if (_locationMap != null)
            _locationMap.LocationChosen -= OpenLevels;
    }

    public void OpenLevels(Location location)
    {
        OpenCleared();
        _levelsMenu.Open();
        _levelsMenu.RenderLocationLevels(location);
    }

    public void OpenCharacteristics()
    {
        OpenCleared();
        _characteristicsMenu.Open();
    }

    public void OpenStore()
    {
        OpenCleared();
        _storeMenu.Open();
    }

    public void OpenLeaderboard()
    {
        OpenCleared();
        _leaderboardMenu.Open();
    }

    public void OpenSettings()
    {
        OpenCleared();
        _settingsMenu.Open();
    }

    private void OpenCleared()
    {
        _levelsMenu.Deactivate();
        _characteristicsMenu.Deactivate();
        _storeMenu.Deactivate();
        _leaderboardMenu.Deactivate();
        _settingsMenu.Deactivate();
        base.Appear();
    }
}
