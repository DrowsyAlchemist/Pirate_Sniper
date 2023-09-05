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
    [SerializeField] private StarsRenderer _locationStarsRenderer;

    private bool _isInitialized;
    private StarsRenderer _generalStarsRenderer;

    public Location Location => _location;
    protected bool IsEnoughStars => _generalStarsRenderer.CurrentStarsCount >= _location.RequiredStars;

    public event Action<Location> Clicked;

    private void Start()
    {
        AddOnClickAction(OnClick);
    }

    private void OnEnable()
    {
        if (_isInitialized)
            Render();
    }

    public void Init(StarsRenderer starsRenderer)
    {
        _iconImage.sprite = _location.Sprite;
        _generalStarsRenderer = starsRenderer;
        _requiredStarsText.text = _location.RequiredStars.ToString();
        _isInitialized = true;
        Render();
        _locationStarsRenderer.Init(this);
    }

    public void Render()
    {
        bool isLocked = IsLocked();
        _lockedPanel.SetActive(isLocked);
        _iconImage.color = isLocked ? Color.gray : Color.white;
        SetInteractable(isLocked == false);
        _locationStarsRenderer.gameObject.SetActive(isLocked == false);
    }

    protected virtual bool IsLocked()
    {
        return (IsEnoughStars && IsPreviousLocationCompleted()) == false;
    }

    protected bool IsPreviousLocationCompleted()
    {
        Location previousLocation = _location.GetPreviousLocation();
        return previousLocation == null || previousLocation.Levels[^1].Score > 0;
    }

    private void OnClick()
    {
        Clicked?.Invoke(_location);
    }
}
