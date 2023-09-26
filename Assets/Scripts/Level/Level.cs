using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LocationsStorage _locationStorage;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private AdditionalMenu _additionalMenu;
    [SerializeField] private LevelsMenu _levelsMenu;
    [SerializeField] private LevelOverWindow _levelOverWindow;
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private LevelInfoRenderer _levelInfoRenderer;
    [SerializeField] private PauseButton _pauseButton;

    private LevelPreset _currentLevel;
    private Saver _saver;
    private Player _player;
    private Sound _sound;

    public LevelObserver LevelObserver { get; private set; }
    public LevelPreset CurrentLevel => _currentLevel;

    public event Action LevelLoaded;

    private void OnDestroy()
    {
        _levelsMenu.LevelClicked -= LoadLevel;

        _levelOverWindow.NextLevelButtonClicked -= OnNextLevelButtonClick;
        _levelOverWindow.RestartButtonClicked -= OnRestartButtonClick;
        _levelOverWindow.MenuButtonClicked -= OnBackToMenuButtonClick;
        _levelOverWindow.DoubleMoneyButtonClick -= OnDoubleMoneyButtonClick;

        LevelObserver.Completed -= OnLevelCompleted;
        _pauseButton.Clicked -= PauseGame;

        _pauseWindow.ResumeButtonClick -= OnResumeButtonClick;
        _pauseWindow.MenuButtonClicked -= OnBackToMenuButtonClick;
        _pauseWindow.SettingsButtonClicked -= OnSettingsButtonClick;
    }

    public void Init(Player player, Saver saver, Sound sound)
    {
        _player = player;
        _saver = saver;
        _sound = sound;

        LevelObserver = new(_player);
        _levelInfoRenderer.Init(_player, LevelObserver);
        _levelOverWindow.Init(_locationStorage);
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
        if (_inputHandler.InputMode == InputMode.Game && Time.timeScale > Settings.Epsilon)
        {
            _pauseWindow.Open();
            Time.timeScale = 0;
            _inputHandler.SetUIMode();
        }
    }

    public int GetLevelScore(LevelPreset levelPreset)
    {
        return _saver.GetLevelScore(levelPreset);
    }

    private void LoadLevel(LevelPreset levelTemplate)
    {
        if (LevelObserver.LevelInstance != null)
            Destroy(LevelObserver.LevelInstance.gameObject);

        _camera.transform.SetPositionAndRotation(levelTemplate.CameraTransform.position, levelTemplate.CameraTransform.rotation);
        _player.Reset();
        _currentLevel = levelTemplate;
        LevelPreset levelInstance = Instantiate(levelTemplate);
        levelInstance.Init(this);
        LevelObserver.SetLevel(levelInstance);
        LevelObserver.Start();
        _mainMenu.Close();
        _levelInfoRenderer.ResetInfo();
        _sound.SetBackgroundMusic(Settings.Sound.ButtleMusic);
        _inputHandler.SetGameMode();
        LevelLoaded?.Invoke();

#if UNITY_EDITOR
        return;
#endif
        Advertising.ShowInter(_sound.BackgroundMusic);
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
        _inputHandler.SetUIMode();
    }

    private void OnNextLevelButtonClick()
    {
        var nextLevel = _locationStorage.GetNextLevel(_currentLevel);

        if (nextLevel == null)
            throw new InvalidOperationException();

        if (_currentLevel.Score > 0)
            LoadLevel(nextLevel);
        else
            Advertising.RewardForVideo(() => LoadLevel(nextLevel), _sound.BackgroundMusic);
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
        _inputHandler.SetUIMode();
    }

    private void OnDoubleMoneyButtonClick()
    {
#if UNITY_EDITOR
        _player.Wallet.Add(LevelObserver.Money);
        _levelOverWindow.Rerender(2 * LevelObserver.Money);
        return;
#endif
        Advertising.RewardForVideo(
            reward: () =>
            {
                _player.Wallet.Add(LevelObserver.Money);
                _levelOverWindow.Rerender(2 * LevelObserver.Money);
            },
            _sound.BackgroundMusic);
    }

    private void OnSettingsButtonClick()
    {
        _additionalMenu.OpenSettings();
    }

    private void OnResumeButtonClick()
    {
        _pauseWindow.Close();
        Time.timeScale = 1;
        _inputHandler.SetGameMode();
    }
}
