using System;
using TMPro;
using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    [SerializeField] private UIButton _button;
    [SerializeField] private TMP_Text _number;
    [SerializeField] private TMP_Text _stars;

    private Level _level;

    public event Action<Level> Clicked;

    private void Awake()
    {
        _button.SetOnClickAction(OnButtonClick);
    }

    public void Render(Level level)
    {
        _level = level;
        _number.text = (level.IndexInLocation + 1).ToString();
        _stars.text = level.Score + " / 3";
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(_level);
    }
}
