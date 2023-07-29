using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LevelOverWindow : AnimatedWindow
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _starsText;
    [SerializeField] private TMP_Text _timeText;

    [SerializeField] private UIButton _menuButton;
    [SerializeField] private UIButton _nextLevelButton;

    private Game _game;

    public event Action MenuButtonClicked;

    public void Init(Game game)
    {
        _game = game;
        _menuButton.SetOnClickAction(() => StartCoroutine(OnMenuButtonClick()));
    }

    public override void Appear()
    {
        Render(_game.CurrentLevelInfo);
        base.Appear();
    }

    private void Render(LevelInfo levelInfo)
    {
        _scoreText.text = levelInfo.Score.ToString();
        _starsText.text = levelInfo._stars + " / 3";
        _timeText.text = levelInfo.Time.ToString();
    }

    private IEnumerator OnMenuButtonClick()
    {
        base.Disappear();

        while (IsPlaying)
            yield return null;

        MenuButtonClicked?.Invoke();
    }
}
