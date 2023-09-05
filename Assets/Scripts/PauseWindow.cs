using System;
using UnityEngine;

public class PauseWindow : Window
{
    [SerializeField] private UIButton _resumeButton;
    [SerializeField] private UIButton _menuButton;
    [SerializeField] private UIButton _settingsButton;

    public event Action ResumeButtonClick;
    public event Action MenuButtonClicked;
    public event Action SettingsButtonClicked;

    public void Init()
    {
        _resumeButton.AddOnClickAction(() => ResumeButtonClick?.Invoke());
        _menuButton.AddOnClickAction(() => MenuButtonClicked?.Invoke());
        _settingsButton.AddOnClickAction(() => SettingsButtonClicked?.Invoke());
        gameObject.SetActive(false);
    }
}
