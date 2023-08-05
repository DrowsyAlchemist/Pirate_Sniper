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

    public void Open(LevelObserver levelObserver)
    {
        Render(levelObserver);
        base.Open();
    }

    public void Render(LevelObserver levelObserver)
    {
        _scoreText.text = levelObserver.Score.ToString();
        _timeText.text = Math.Round(levelObserver.CompleteTime, 2).ToString();
        _headshotsText.text = levelObserver.HeadShots.ToString();
        _accuracyText.text = (int)(levelObserver.Accuracy * 100) + " %";
        _moneyText.text = "+ " + levelObserver.Money.ToString();

        int i = 0;

        for (; i < levelObserver.Stars; i++)
            _stars[i].Activate();

        for (; i < _stars.Length; i++)
            _stars[i].Deactivate();
    }
}
