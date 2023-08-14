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
        _resumeButton.SetOnClickAction(() => ResumeButtonClick?.Invoke());
        _menuButton.SetOnClickAction(() => MenuButtonClicked?.Invoke());
        _settingsButton.SetOnClickAction(() => SettingsButtonClicked?.Invoke());
        gameObject.SetActive(false);
    }
}
