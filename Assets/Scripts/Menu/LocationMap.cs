using System;
using System.Collections.Generic;
using UnityEngine;

public class LocationMap : Window
{
    [SerializeField] private LocationsStorage _locationsStorage;
    [SerializeField] private StarsRenderer _starsRenderer;
    [SerializeField] private List<LocationButton> _locationButtons;

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
        _starsRenderer.Init(_locationButtons.ToArray());

        foreach (var locationButton in _locationButtons)
            locationButton.Init(_starsRenderer, _locationsStorage);
    }

    private void OnLocationButtonClick(Location location)
    {
        LocationChosen?.Invoke(location);
    }
}
