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

    public void Appear(LevelInfo levelInfo)
    {
        Render(levelInfo);
        base.Appear();
    }

    private void Render(LevelInfo levelInfo)
    {
        _scoreText.text = levelInfo.Score.ToString();
        _starsText.text = levelInfo.Stars + " / 3";
        _timeText.text = levelInfo.CompleteTime.ToString();
        _headshotsText.text = levelInfo.HeadShots.ToString();
        _accuracyText.text = (int)(levelInfo.Accuracy * 100) + " %";
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
