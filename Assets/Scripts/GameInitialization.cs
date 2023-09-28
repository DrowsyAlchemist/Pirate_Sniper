using Agava.WebUtility;
using Agava.YandexGames;
using Lean.Localization;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameInitialization : MonoBehaviour
{
    [SerializeField] private bool _useMobileControlInEditor;
    [SerializeField] private InputHandler _inputController;
    [SerializeField] private Sensitivity _sensitivity;
    [SerializeField] private ShootingPoint _shootingPoint;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Level _level;
    [SerializeField] private LocationsStorage _locationsStorage;
    [SerializeField] private Sound _sound;

    [SerializeField] private Canvas _backgroundCanvas;

    private Saver _saver;

    private IEnumerator Start()
    {
#if UNITY_EDITOR
        yield return Init();
        yield break;
#endif
        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();

        yield return Init();
    }

    private IEnumerator Init()
    {
        SetLanguage();
        _saver = new Saver(_locationsStorage);

        while (_saver.IsReady == false)
            yield return null;

        bool isMobile = IsMobile();
        _sensitivity.Init(_saver, isMobile);
        _inputController.Init(isMobile, _sensitivity);
        _sound.Init();
        var player = CreatePlayer(_saver);
        _level.Init(player, _saver, _sound);
        _locationsStorage.Init(_level);
        _mainMenu.Init(player, _saver, _sound);
        _mainMenu.Open();
        _shootingPoint.Init(_saver, _inputController, player);
        _backgroundCanvas.Deactivate();
    }

    public void RemoveSaves()
    {
        _saver.RemoveSaves();
    }

    public void UnlockAllLevels()
    {
        _saver.UnlockAllLevels();
    }

    private void SetLanguage()
    {
#if !UNITY_EDITOR
        string currentLang = YandexGamesSdk.Environment.GetCurrentLang();
        LeanLocalization.SetCurrentLanguageAll(currentLang);
#endif
    }

    private bool IsMobile()
    {
#if UNITY_EDITOR
        return _useMobileControlInEditor;
#else
        return Device.IsMobile;
#endif
    }

    private Player CreatePlayer(Saver saver)
    {
        Health health = new(saver.PlayerHealth);
        Wallet wallet = new(saver, _sound.BuySound);
        return new Player(saver, health, wallet, _shootingPoint);
    }
}
