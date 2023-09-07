using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelsMenu _levelsMenu;
    [SerializeField] private LevelOverWindow _levelOverWindow;
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private LevelInfoRenderer _levelInfoRenderer;
    [SerializeField] private PauseButton _pauseButton;

    private static Level _instance;
    private LevelPreset _currentLevel;
    private Saver _saver;
    private Player _player;

    public LevelObserver LevelObserver { get; private set; }
    public LevelPreset CurrentLevel => _currentLevel;

    public event Action LevelLoaded;

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
        _levelOverWindow.DoubleMoneyButtonClick -= OnDoubleMoneyButtonClick;

        LevelObserver.Completed -= OnLevelCompleted;
        _pauseButton.Clicked -= OnBackToMenuButtonClick;

        _pauseWindow.ResumeButtonClick -= OnResumeButtonClick;
        _pauseWindow.MenuButtonClicked -= OnBackToMenuButtonClick;
        _pauseWindow.SettingsButtonClicked -= OnSettingsButtonClick;
    }

    public void Init(Player player, Saver saver)
    {
        _player = player;
        _saver = saver;

        LevelObserver = new(_player);
        _levelInfoRenderer.Init(_player, LevelObserver);
        _levelOverWindow.Init();
        _pauseWindow.Init();

        _levelsMenu.LevelClicked += LoadLevel;
        _levelOverWindow.NextLevelButtonClicked += OnNextLevelButtonClick;
        _levelOverWindow.RestartButtonClicked += OnRestartButtonClick;
        _levelOverWindow.MenuButtonClicked += OnBackToMenuButtonClick;
        _levelOverWindow.DoubleMoneyButtonClick += OnDoubleMoneyButtonClick;
        LevelObserver.Completed += OnLevelCompleted;
        _pauseButton.Clicked += PauseGame;

        _pauseWindow.ResumeButtonClick += OnResumeButtonClick;
        _pauseWindow.MenuButtonClicked += OnBackToMenuButtonClick;
        _pauseWindow.SettingsButtonClicked += OnSettingsButtonClick;
    }

    public void PauseGame()
    {
        _pauseWindow.Open();
        Time.timeScale = 0;
        InputController.SetMode(InputMode.UI);
    }

    public static int GetLevelScore(LevelPreset levelPreset)
    {
        return _instance._saver.GetLevelScore(levelPreset);
    }

    private void LoadLevel(LevelPreset levelTemplate)
    {
        if (LevelObserver.LevelInstance != null)
            Destroy(LevelObserver.LevelInstance.gameObject);

        _camera.transform.SetPositionAndRotation(levelTemplate.CameraTransform.position, levelTemplate.CameraTransform.rotation);
        _player.Reset();
        _currentLevel = levelTemplate;
        LevelObserver.SetLevel(Instantiate(levelTemplate));
        LevelObserver.Start();
        _mainMenu.Close();
        _levelInfoRenderer.ResetInfo();
        Sound.SetBackgroundMusic(Settings.Sound.ButtleMusic);
        InputController.SetMode(InputMode.Game);
        LevelLoaded?.Invoke();

#if UNITY_EDITOR
        return;
#endif
        Advertising.ShowInter();
    }

    private void OnLevelCompleted(bool isWon)
    {
        if (isWon)
        {
            _player.Wallet.Add(LevelObserver.Money);

            if (LevelObserver.Score > _saver.GetLevelScore(_currentLevel))
                _saver.SaveLevel(_currentLevel, LevelObserver.Score);
        }
        _levelOverWindow.Appear(LevelObserver);
        InputController.SetMode(InputMode.UI);
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
        LoadLevel(_currentLevel);
    }

    private void OnBackToMenuButtonClick()
    {
        var levelInstance = LevelObserver.LevelInstance;
        LevelObserver.Clear();
        Destroy(levelInstance.gameObject);
        Time.timeScale = 1;
        _pauseWindow.Close();
        _mainMenu.Open();
        InputController.SetMode(InputMode.UI);
    }

    private void OnDoubleMoneyButtonClick()
    {
#if UNITY_EDITOR
        _player.Wallet.Add(LevelObserver.Money);
        return;
#endif
        Advertising.RewardForVideo(() => _player.Wallet.Add(LevelObserver.Money));
    }

    private void OnSettingsButtonClick()
    {
        _mainMenu.OpenSettings();
    }

    private void OnResumeButtonClick()
    {
        _pauseWindow.Close();
        Time.timeScale = 1;
        InputController.SetMode(InputMode.Game);
    }
}
