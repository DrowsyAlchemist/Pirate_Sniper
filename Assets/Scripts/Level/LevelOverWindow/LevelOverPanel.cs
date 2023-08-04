using System;
using UnityEngine;

public class LevelOverPanel : Window
{
    [SerializeField] private UIButton _nextLevelButton;
    [SerializeField] private UIButton _restartButton;
    [SerializeField] private UIButton _menuButton;

    public event Action NextLevelButtonClicked;
    public event Action RestartButtonClicked;
    public event Action MenuButtonClicked;

    public void Init()
    {
        _nextLevelButton.SetOnClickAction(() => NextLevelButtonClicked?.Invoke());
        _restartButton.SetOnClickAction(() => RestartButtonClicked?.Invoke());
        _menuButton.SetOnClickAction(() => MenuButtonClicked?.Invoke());
    }
}
