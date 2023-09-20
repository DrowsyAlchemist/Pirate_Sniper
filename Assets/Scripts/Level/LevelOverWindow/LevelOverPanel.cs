using System;
using UnityEngine;

public class LevelOverPanel : Window
{
    [SerializeField] private UIButton _nextLevelButton;
    [SerializeField] private UIButton _restartButton;
    [SerializeField] private UIButton _menuButton;

    [SerializeField] private AudioSource _openSound;

    private LocationsStorage _locationStorage;

    public event Action NextLevelButtonClicked;
    public event Action RestartButtonClicked;
    public event Action MenuButtonClicked;

    public virtual void Init(LocationsStorage locationsStorage)
    {
        _locationStorage = locationsStorage;
        _nextLevelButton.AddOnClickAction(() => NextLevelButtonClicked?.Invoke());
        _restartButton.AddOnClickAction(() => RestartButtonClicked?.Invoke());
        _menuButton.AddOnClickAction(() => MenuButtonClicked?.Invoke());
    }

    public virtual void Open(LevelObserver levelObserver)
    {
        bool hasNextLevel = _locationStorage.GetNextLevel(levelObserver.LevelInstance) != null;
        _nextLevelButton.SetInteractable(hasNextLevel);
        _openSound.Play();
        base.Open();
    }
}
