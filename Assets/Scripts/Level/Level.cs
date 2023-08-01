using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelsMenu _levelsMenu;
    [SerializeField] private LevelOverWindow _levelOverWindow;
    [SerializeField] private LevelInfoRenderer _levelInfoRenderer;

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
    }

    public void Init(Player player, Saver saver)
    {
        _player = player;
        _saver = saver;

        _levelObserver = new(_player);
        _levelInfoRenderer.Init(_player, _levelObserver);
        _levelOverWindow.Init();

        _levelsMenu.LevelClicked += LoadLevel;
        _levelOverWindow.NextLevelButtonClicked += OnNextLevelButtonClick;
        _levelOverWindow.RestartButtonClicked += OnRestartButtonClick;
        _levelOverWindow.MenuButtonClicked += OnBackToMenuButtonClick;
        _levelObserver.Completed += OnLevelCompleted;
    }

    public static int GetLevelScore(LevelPreset levelPreset)
    {
        return _instance._saver.GetLevelScore(levelPreset);
    }

    private void LoadLevel(LevelPreset levelTemplate)
    {
        _player.Reset();
        _currentLevel = levelTemplate;
        _levelObserver.SetLevel(Instantiate(levelTemplate));
        _levelObserver.Start();
        _mainMenu.Close();
        _levelInfoRenderer.ResetInfo();
    }

    private void OnLevelCompleted()
    {
        _levelOverWindow.Appear(_levelObserver);
        _player.Wallet.Add(_levelObserver.Money);

        if (_levelObserver.Score > _saver.GetLevelScore(_currentLevel))
            _saver.SaveLevel(_currentLevel, _levelObserver.Score);
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

    private void OnRestartButtonClick()
    {
        LoadLevel(_currentLevel);
    }

    private void OnBackToMenuButtonClick()
    {
        _mainMenu.Open();
        Destroy(_levelObserver.LevelInstance.gameObject);
    }
}
