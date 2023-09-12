using System;
using System.Collections;
using UnityEngine;

public class AdditionalMenu : AnimatedWindow
{
    [SerializeField] private LevelsMenu _levelsMenu;
    [SerializeField] private CharacteristicsMenu _characteristicsMenu;
    [SerializeField] private StoreMenu _storeMenu;
    [SerializeField] private LeaderboardMenu _leaderboardMenu;
    [SerializeField] private SettingsMenu _settingsMenu;

    [SerializeField] UIButton _closeButton;

    private LocationMap _locationMap;

    public event Action ForceClosed;

    public void Init(Player player, Saver saver, LocationMap locationMap)
    {
        gameObject.SetActive(true);
        _locationMap = locationMap;
        _locationMap.LocationChosen += OpenLevels;
        _closeButton.AddOnClickAction(Disappear);

        _characteristicsMenu.Init(player);
        _storeMenu.Init(player.Wallet, saver);
        _leaderboardMenu.Init(saver);
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

    public override void Disappear()
    {
        base.Disappear();

        if (Time.timeScale < Settings.Epsilon)
            Settings.CoroutineObject.StartCoroutine(ForseClose());
    }

    private IEnumerator ForseClose()
    {
        while (IsPlaying)
            yield return null;

        ForceClosed?.Invoke();
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
