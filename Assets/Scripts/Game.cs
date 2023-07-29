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
    private Saver _saver;
    private Level _currentLevel;
    private LevelInfo _currentLevelInfo;

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
        _levelOverWindow.NextLevelButtonClicked += OnNextLevelButtonClick;
        _saver = new Saver();
        _levelsMenu.Init(_saver);
        _saver.SaveLevel(0, 0, 123);
        _saver.SaveLevel(0, 1, 1223);
    }

    public static int GetLevelScore(Level level)
    {
        return _instance._saver.GetLevelScore(level);
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
        _currentLevel = levelTemplate;
        _currentLevelInfo = new(Instantiate(levelTemplate));
        _currentLevelInfo.Completed += OnLevelCompleted;
        _mainMenu.Close();
    }

    private void OnLevelCompleted()
    {
        _currentLevelInfo.Completed -= OnLevelCompleted;
        _levelOverWindow.Appear(_currentLevelInfo);
    }

    private void OnBackToMenuButtonClick()
    {
        _mainMenu.Open();
        _saver.SaveLevel(_currentLevel.Location.Index, _currentLevel.IndexInLocation, 98);
        Destroy(_currentLevelInfo.LevelInstance.gameObject);
    }


    private void OnNextLevelButtonClick()
    {
        throw new NotImplementedException();
    }
}
