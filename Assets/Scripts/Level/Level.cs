using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelsMenu _levelsMenu;
    [SerializeField] private LevelOverWindow _levelOverWindow;
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private LevelInfoRenderer _levelInfoRenderer;
    [SerializeField] private PauseButton _pauseButton;

    private static Level _instance;
    private LevelPreset _currentLevel;
    private LevelObserver _levelObserver;
    private Saver _saver;
    private Player _player;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _levelsMenu.LevelClicked -= LoadLevel;
        _levelOverWindow.NextLevelButtonClicked -= OnNextLevelButtonClick;
        _levelOverWindow.RestartButtonClicked -= OnRestartButtonClick;
        _levelOverWindow.MenuButtonClicked -= OnBackToMenuButtonClick;
        _levelObserver.Completed -= OnLevelCompleted;
        _pauseButton.Clicked -= OnBackToMenuButtonClick;
        _pauseWindow.MenuButtonClicked -= OnBackToMenuButtonClick;
        _pauseWindow.SettingsButtonClicked -= OnSettingsButtonClick;
    }

    public void Init(Player player, Saver saver)
    {
        _player = player;
        _saver = saver;

        _levelObserver = new(_player);
        _levelInfoRenderer.Init(_player, _levelObserver);
        _levelOverWindow.Init();
        _pauseWindow.Init();

        _levelsMenu.LevelClicked += LoadLevel;
        _levelOverWindow.NextLevelButtonClicked += OnNextLevelButtonClick;
        _levelOverWindow.RestartButtonClicked += OnRestartButtonClick;
        _levelOverWindow.MenuButtonClicked += OnBackToMenuButtonClick;
        _levelObserver.Completed += OnLevelCompleted;
        _pauseButton.Clicked += OnPauseButtonClick;
        _pauseWindow.MenuButtonClicked += OnBackToMenuButtonClick;
        _pauseWindow.SettingsButtonClicked += OnSettingsButtonClick;
    }

    public static int GetLevelScore(LevelPreset levelPreset)
    {
        return _instance._saver.GetLevelScore(levelPreset);
    }

    private void LoadLevel(LevelPreset levelTemplate)
    {
        if (_levelObserver.LevelInstance != null)
            Destroy(_levelObserver.LevelInstance.gameObject);

        _player.Reset();
        _currentLevel = levelTemplate;
        _levelObserver.SetLevel(Instantiate(levelTemplate));
        _levelObserver.Start();
        _mainMenu.Close();
        _levelInfoRenderer.ResetInfo();
        Sound.SetBackgroundMusic(Settings.Sound.ButtleMusic);
#if UNITY_EDITOR
        return;
#endif
        Advertising.ShowInter();
    }

    private void OnLevelCompleted(bool isWon)
    {
        if (isWon)
        {
            _player.Wallet.Add(_levelObserver.Money);

            if (_levelObserver.Score > _saver.GetLevelScore(_currentLevel))
                _saver.SaveLevel(_currentLevel, _levelObserver.Score);
        }
        _levelOverWindow.Appear(_levelObserver);
    }

    private void OnNextLevelButtonClick()
    {
        if (_currentLevel.TryGetNextLevel(out LevelPreset nextLevel))
        {
            if (_currentLevel.Score > 0)
                LoadLevel(nextLevel);
            else
                Advertising.RewardForVideo(() => LoadLevel(nextLevel));
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    private void OnRestartButtonClick()
    {
        Advertising.RewardForVideo(reward: () => LoadLevel(_currentLevel));
    }

    private void OnBackToMenuButtonClick()
    {
        var levelInstance = _levelObserver.LevelInstance;
        _levelObserver.Clear();
        Destroy(levelInstance.gameObject);
        _mainMenu.Open();
    }

    private void OnPauseButtonClick()
    {
        _pauseWindow.Open();
    }

    private void OnSettingsButtonClick()
    {
        _mainMenu.OpenSettings();
    }
}
