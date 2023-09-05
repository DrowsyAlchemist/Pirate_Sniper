using System;
using UnityEngine;

public class LevelOverPanel : Window
{
    [SerializeField] private UIButton _nextLevelButton;
    [SerializeField] private UIButton _restartButton;
    [SerializeField] private UIButton _menuButton;

    [SerializeField] private AudioSource _openSound;

    public event Action NextLevelButtonClicked;
    public event Action RestartButtonClicked;
    public event Action MenuButtonClicked;

    public virtual void Init()
    {
        _nextLevelButton.AddOnClickAction(() => NextLevelButtonClicked?.Invoke());
        _restartButton.AddOnClickAction(() => RestartButtonClicked?.Invoke());
        _menuButton.AddOnClickAction(() => MenuButtonClicked?.Invoke());
    }

    public virtual void Open(LevelObserver levelObserver)
    {
        _nextLevelButton.SetInteractable(levelObserver.LevelInstance.TryGetNextLevel(out LevelPreset _));
        _openSound.Play();
        base.Open();
    }
}
