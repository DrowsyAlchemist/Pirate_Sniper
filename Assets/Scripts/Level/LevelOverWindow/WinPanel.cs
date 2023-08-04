using TMPro;
using UnityEngine;

public class WinPanel : LevelOverPanel
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _starsText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _headshotsText;
    [SerializeField] private TMP_Text _accuracyText;
    [SerializeField] private TMP_Text _moneyText;

    public void Open(LevelObserver levelObserver)
    {
        Render(levelObserver);
        base.Open();
    }

    public void Render(LevelObserver levelObserver)
    {
        _scoreText.text = levelObserver.Score.ToString();
        _starsText.text = levelObserver.Stars + " / 3";
        _timeText.text = levelObserver.CompleteTime.ToString();
        _headshotsText.text = levelObserver.HeadShots.ToString();
        _accuracyText.text = (int)(levelObserver.Accuracy * 100) + " %";
        _moneyText.text = "+ " + levelObserver.Money.ToString();
    }
}
