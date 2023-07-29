using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private bool _useMobileControlInEditor;
    [SerializeField] private InputController _inputController;
    [SerializeField] private HomeButton _menuButton;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelsMenu _levelsMenu;
    [SerializeField] private LevelOverWindow _levelOverWindow;

    private static Game _instance;
    private Player _player;
    private SaveSystem _saveSystem;

    public LevelInfo CurrentLevelInfo { get; private set; }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private IEnumerator Start()
    {
#if UNITY_EDITOR
        Init();
        yield break;
#endif
        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();

        Init();
    }


    private void Init()
    {
        InitInputController();
        _player = new Player(_inputController, 100, 50);
        _levelsMenu.LevelClicked += LoadLevel;
        _levelOverWindow.Init(this);
        _levelOverWindow.MenuButtonClicked += OnBackToMenuButtonClick;
        _saveSystem = new SaveSystem();
        _saveSystem.SaveLevel(0, 0, 123);
        _saveSystem.SaveLevel(0, 1, 1223);
        _saveSystem.Save();
    }

    private void InitInputController()
    {
#if UNITY_EDITOR
        bool isMobile = _useMobileControlInEditor;
#else
        bool isMobile = Device.IsMobile; 
#endif
        _inputController.Init(isMobile);
        _menuButton.Init(isMobile);
    }

    private void LoadLevel(Level levelTemplate)
    {
        var level = Instantiate(levelTemplate);
        level.Init();
        level.Completed += OnLevelCompleted;
        CurrentLevelInfo = new LevelInfo(level);
        _mainMenu.Close();
    }

    private void OnLevelCompleted()
    {
        _levelOverWindow.Appear();
    }

    private void OnBackToMenuButtonClick()
    {
        _mainMenu.Open();
        Destroy(CurrentLevelInfo.Level);
    }
}
