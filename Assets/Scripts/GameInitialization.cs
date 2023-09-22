using Agava.WebUtility;
using Agava.YandexGames;
using cakeslice;
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
    [SerializeField] private TMP_Text _errorText;

    private static GameInitialization _instance;//
    private Saver _saver;

    public static TMP_Text ErrorText => _instance._errorText;//

    private IEnumerator Start()
    {
        _instance = this;//
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
        try
        {
            SetLanguage();
            _saver = new Saver(_locationsStorage); _errorText.text = "Saver created";

        }
        catch (Exception e)
        {
            _errorText.text = "Saver error: " + e.Message;
            _errorText.Activate();
        }
        while (_saver.IsReady == false)
            yield return null;

        try
        {
            bool isMobile = IsMobile(); _errorText.text += " 60";
            _sensitivity.Init(_saver, isMobile); _errorText.text += " 61";
            _inputController.Init(isMobile, _sensitivity); _errorText.text += " 62";
            _sound.Init(); _errorText.text += " 63";
            var player = CreatePlayer(_saver); _errorText.text += " 64";
            _level.Init(player, _saver, _sound); _errorText.text += " 65";
            _mainMenu.Init(player, _saver, _sound); _errorText.text += " 66";
            _mainMenu.Open(); _errorText.text += " 67";
            _shootingPoint.Init(_saver, _inputController, player); _errorText.text += " 68";
            _backgroundCanvas.Deactivate(); _errorText.text += " 69";
        }
        catch (Exception e)
        {
            _errorText.text += e.Message;
            _errorText.Activate();
        }
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
