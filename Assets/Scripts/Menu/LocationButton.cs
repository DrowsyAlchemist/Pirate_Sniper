using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationButton : UIButton
{
    [SerializeField] private Location _location;
    [SerializeField] private Image _iconImage;
    [SerializeField] private RectTransform _lockedPanel;
    [SerializeField] private TMP_Text _requiredStarsText;

    private bool _isInitialized;
    private StarsRenderer _starsRenderer;

    public Location Location => _location;

    public event Action<Location> Clicked;

    private void Start()
    {
        SetOnClickAction(OnClick);
    }

    private void OnEnable()
    {
        if (_isInitialized)
            Render();
    }

    public void Init(StarsRenderer starsRenderer)
    {
        _iconImage.sprite = _location.Sprite;
        _starsRenderer = starsRenderer;
        _requiredStarsText.text = _location.RequiredStars.ToString();
        _isInitialized = true;
        Render();
    }

    public void Render()
    {
        bool isLocked = _starsRenderer.CurrentStarsCount < _location.RequiredStars;
        _lockedPanel.SetActive(isLocked);
        _iconImage.color = isLocked ? Color.gray : Color.white;
        SetInteractable(isLocked == false);
    }

    private void OnClick()
    {
        Clicked?.Invoke(_location);
    }
}
