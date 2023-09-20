using System;
using System.Collections;
using UnityEngine;

public class LevelOverWindow : AnimatedWindow
{
    [SerializeField] private LevelOverPanel _losePanel;
    [SerializeField] private WinPanel _winPanel;

    public event Action NextLevelButtonClicked;
    public event Action RestartButtonClicked;
    public event Action MenuButtonClicked;
    public event Action DoubleMoneyButtonClick;

    public void Init(LocationsStorage locationsStorage)
    {
        InitLevelOverPanel(_losePanel, locationsStorage);
        InitLevelOverPanel(_winPanel, locationsStorage);
        _winPanel.DoubleMoneyButtonClick += () => DoubleMoneyButtonClick.Invoke();
        gameObject.SetActive(true);
    }

    public void Appear(LevelObserver levelObserver)
    {
        Render(levelObserver);
        base.Appear();
    }

    private void InitLevelOverPanel(LevelOverPanel panel, LocationsStorage locationsStorage)
    {
        panel.Init(locationsStorage);
        panel.NextLevelButtonClicked += OnNextLevelButtonClick;
        panel.RestartButtonClicked += OnRestartButtonClick;
        panel.MenuButtonClicked += OnMenuButtonClick;
        panel.Close();
    }

    private void Render(LevelObserver levelObserver)
    {
        if (levelObserver.IsWon)
            _winPanel.Open(levelObserver);
        else
            _losePanel.Open(levelObserver);
    }

    private void OnNextLevelButtonClick()
    {
        StartCoroutine(DoActionAfterDisappear(() => NextLevelButtonClicked?.Invoke()));
    }

    private void OnRestartButtonClick()
    {
        StartCoroutine(DoActionAfterDisappear(() => RestartButtonClicked?.Invoke()));
    }

    private void OnMenuButtonClick()
    {
        StartCoroutine(DoActionAfterDisappear(() => MenuButtonClicked?.Invoke()));
    }

    private IEnumerator DoActionAfterDisappear(Action action)
    {
        base.Disappear();

        while (IsPlaying)
            yield return null;

        _losePanel.Close();
        _winPanel.Close();
        action();
    }
}
