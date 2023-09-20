using System;
using TMPro;
using UnityEngine;

public class LevelRenderer : MonoBehaviour
{
    [SerializeField] private UIButton _button;
    [SerializeField] private TMP_Text _number;
    [SerializeField] private RectTransform _lockedPanel;

    [SerializeField] private RectTransform[] _stars;

    private LevelPreset _level;

    public event Action<LevelPreset> Clicked;

    private void Awake()
    {
        _button.AddOnClickAction(OnButtonClick);
    }

    public void Render(LevelPreset level, LocationsStorage locationsStorage)
    {
        _level = level ?? throw new ArgumentNullException();
        _number.text = (locationsStorage.GetIndexInLocation(level) + 1).ToString();
        bool isLocked = IsLocked(level, locationsStorage);
        _lockedPanel.gameObject.SetActive(isLocked);
        _button.SetInteractable(isLocked == false);
        RenderStars();
    }

    private bool IsLocked(LevelPreset level, LocationsStorage locationsStorage)
    {
        var previousLevel = locationsStorage.GetPreviousLevel(level);
        return (locationsStorage.GetIndexInLocation(level) != 0) && (level.Score == 0) && (previousLevel.Score == 0);
    }

    private void RenderStars()
    {
        int i = 0;

        for (; i < _level.Stars; i++)
            _stars[i].Activate();

        for (; i < _stars.Length; i++)
            _stars[i].Deactivate();
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke(_level);
    }
}
