using System;
using TMPro;
using UnityEngine;

public class WinPanel : LevelOverPanel
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _headshotsText;
    [SerializeField] private TMP_Text _accuracyText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private RectTransform[] _stars;

    [SerializeField] private UIButton _doubleMoneyButton;

    private const int IncreaseMoneyModifier = 2;
    private LevelObserver _levelObserver;

    public event Action DoubleMoneyButtonClick;

    public override void Init()
    {
        base.Init();
        _doubleMoneyButton.AddOnClickAction(OnDoubleMoneyButtonClick);
    }

    public override void Open(LevelObserver levelObserver)
    {
        Render(levelObserver);
        base.Open(levelObserver);
    }

    public void Render(LevelObserver levelObserver)
    {
        _levelObserver = levelObserver;
        _scoreText.text = levelObserver.Score.ToString();
        _timeText.text = Math.Round(levelObserver.CompleteTime, 2).ToString();
        _headshotsText.text = levelObserver.HeadShots.ToString();
        _accuracyText.text = (int)(levelObserver.Accuracy * 100) + " %";
        _moneyText.text = "+" + levelObserver.Money.ToString();
        _doubleMoneyButton.gameObject.SetActive(levelObserver.Money > 0);
        _doubleMoneyButton.SetInteractable(true);

        int i = 0;

        for (; i < levelObserver.Stars; i++)
            _stars[i].Activate();

        for (; i < _stars.Length; i++)
            _stars[i].Deactivate();
    }

    private void OnDoubleMoneyButtonClick()
    {
        _doubleMoneyButton.SetInteractable(false);
        DoubleMoneyButtonClick?.Invoke();
        _moneyText.text = "+" + (IncreaseMoneyModifier * _levelObserver.Money).ToString();
    }
}
