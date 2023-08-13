using System;
using UnityEngine;

public class PauseWindow : Window
{
    [SerializeField] private UIButton _resumeButton;
    [SerializeField] private UIButton _menuButton;
    [SerializeField] private UIButton _settingsButton;

    public event Action MenuButtonClicked;
    public event Action SettingsButtonClicked;

    public void Init()
    {
        _resumeButton.SetOnClickAction(Close);
        _menuButton.SetOnClickAction(OnMenuButtonClick);
        _settingsButton.SetOnClickAction(() => SettingsButtonClicked?.Invoke());
        gameObject.SetActive(false);
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1;
    }

    public override void Open()
    {
        base.Open();
        Time.timeScale = 0;
    }

    private void OnMenuButtonClick()
    {
        MenuButtonClicked?.Invoke();
        Close();
    }
}
