using System;
using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    [SerializeField] private UIButton _button;

    private Level _level;

    public event Action<Level> Clicked;

    private void Awake()
    {
        _button.SetOnClickAction(OnButtonClick);
    }

    public void Render(Level level)
    {
        _level = level;
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(_level);
    }
}
