using Agava.WebUtility;
using Agava.YandexGames;
using cakeslice;
using Lean.Localization;
using System.Collections;
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
    [SerializeField] private OutlineEffect _outlineEffect;

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
        InitOutlineEffect(isMobile);
        _sensitivity.Init(_saver, isMobile);
        _inputController.Init(isMobile, _sensitivity);
        var player = CreatePlayer(_saver);
        _level.Init(player, _saver);
        _mainMenu.Init(player, _saver);
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
        return = Device.IsMobile;
#endif
    }

    private void InitOutlineEffect(bool isMobile)
    {
        if (isMobile == false)
            Destroy(_outlineEffect);
    }

    private Player CreatePlayer(Saver saver)
    {
        Health health = new(saver.PlayerHealth);
        Wallet wallet = new(saver);
        return new Player(saver, health, wallet, _shootingPoint);
    }
}
