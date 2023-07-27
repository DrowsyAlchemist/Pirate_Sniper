using System;
using UnityEngine;

public class LocationButton : UIButton
{
    [SerializeField] private Location _location;

    public Location Location => _location;

    public event Action<Location> Clicked;

    private void Start()
    {
        SetOnClickAction(OnClick);
    }

    private void OnClick()
    {
        Clicked?.Invoke(_location);
    }
}
