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
    [SerializeField] private LevelInfoRenderer _levelInfoRenderer;

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

    private void OnDestroy()
    {
        _levelsMenu.LevelClicked -= LoadLevel;
        _levelOverWindow.MenuButtonClicked -= OnBackToMenuButtonClick;
        _levelOverWindow.NextLevelButtonClicked -= OnNextLevelButtonClick;
        _currentLevelInfo.Completed -= OnLevelCompleted;
    }


    private void Init()
    {
        InitInputController();
        _player = new Player(_inputController, 100, 50);
        _levelsMenu.LevelClicked += LoadLevel;
        _levelOverWindow.Init();
        _levelOverWindow.MenuButtonClicked += OnBackToMenuButtonClick;
        _levelOverWindow.NextLevelButtonClicked += OnNextLevelButtonClick;
        _saver = new Saver();
        _levelsMenu.Init(_saver);
        _currentLevelInfo = new(_player);
        _currentLevelInfo.Completed += OnLevelCompleted;
        _levelInfoRenderer.Init(_currentLevelInfo);
    }

    public void RemoveSaves()
    {
        _saver.RemoveSaves();
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
        _currentLevelInfo.SetLevel(Instantiate(levelTemplate));
        _currentLevelInfo.Start();
        _mainMenu.Close();
        _levelInfoRenderer.ResetInfo();
    }

    private void OnLevelCompleted()
    {
        _levelOverWindow.Appear(_currentLevelInfo);

        if (_currentLevelInfo.Score > _saver.GetLevelScore(_currentLevel))
            _saver.SaveLevel(_currentLevel, _currentLevelInfo.Score);
    }

    private void OnBackToMenuButtonClick()
    {
        _mainMenu.Open();
        Destroy(_currentLevelInfo.LevelInstance.gameObject);
    }


    private void OnNextLevelButtonClick()
    {
        Location currentLocation = _currentLevel.Location;
        int nextLevelIndex = _currentLevel.IndexInLocation + 1;

        if (nextLevelIndex < currentLocation.Levels.Count)
        {
            LoadLevel(currentLocation.GetLevelByIndex(nextLevelIndex));
        }
        else
        {
            int nextLocationIndex = currentLocation.Index + 1;
            Location nextLocation = LocationsStorage.GetLocationByIndex(nextLocationIndex);
            LoadLevel(nextLocation.GetLevelByIndex(0));
        }
    }
}
