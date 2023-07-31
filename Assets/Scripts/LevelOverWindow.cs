using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelOverWindow : AnimatedWindow
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _starsText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _headshotsText;
    [SerializeField] private TMP_Text _accuracyText;
    [SerializeField] private TMP_Text _moneyText;

    [SerializeField] private UIButton _nextLevelButton;
    [SerializeField] private UIButton _restartButton;
    [SerializeField] private UIButton _menuButton;

    public event Action NextLevelButtonClicked;
    public event Action RestartButtonClicked;
    public event Action MenuButtonClicked;

    public void Init()
    {
        _nextLevelButton.SetOnClickAction(OnNextLevelButtonClick);
        _restartButton.SetOnClickAction(OnRestartButtonClick);
        _menuButton.SetOnClickAction(OnMenuButtonClick);
    }

    public void Appear(LevelObserver levelObserver)
    {
        Render(levelObserver);
        base.Appear();
    }

    private void Render(LevelObserver levelObserver)
    {
        _scoreText.text = levelObserver.Score.ToString();
        _starsText.text = levelObserver.Stars + " / 3";
        _timeText.text = levelObserver.CompleteTime.ToString();
        _headshotsText.text = levelObserver.HeadShots.ToString();
        _accuracyText.text = (int)(levelObserver.Accuracy * 100) + " %";
        _moneyText.text = "+ " + levelObserver.Money.ToString();
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

        action();
    }
}
