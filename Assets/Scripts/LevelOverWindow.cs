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

    [SerializeField] private UIButton _menuButton;
    [SerializeField] private UIButton _nextLevelButton;

    public event Action MenuButtonClicked;
    public event Action NextLevelButtonClicked;

    public void Init()
    {
        _menuButton.SetOnClickAction(() => StartCoroutine(OnMenuButtonClick()));
        _nextLevelButton.SetOnClickAction(() => StartCoroutine(OnNextLevelButtonClick()));
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
    }

    private IEnumerator OnMenuButtonClick()
    {
        base.Disappear();

        while (IsPlaying)
            yield return null;

        MenuButtonClicked?.Invoke();
    }

    private IEnumerator OnNextLevelButtonClick()
    {
        base.Disappear();

        while (IsPlaying)
            yield return null;

        NextLevelButtonClicked?.Invoke();
    }
}
