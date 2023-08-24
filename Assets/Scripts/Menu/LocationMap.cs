using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationMap : MonoBehaviour
{
    [SerializeField] private List<LocationButton> _locationButtons;
    [SerializeField] private StarsRenderer _starsRenderer;

    public event Action<Location> LocationChosen;

    private void Awake()
    {
        foreach (var locationButton in _locationButtons)
            locationButton.Clicked += OnLocationButtonClick;
    }

    private void OnDestroy()
    {
        foreach (var locationButton in _locationButtons)
            locationButton.Clicked -= OnLocationButtonClick;
    }

    public void Init()
    {
        _starsRenderer.Init(_locationButtons);

        foreach (var locationButton in _locationButtons)
            locationButton.Init(_starsRenderer);
    }

    private void OnLocationButtonClick(Location location)
    {
        LocationChosen?.Invoke(location);
    }
}
