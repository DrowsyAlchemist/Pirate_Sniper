using System.Collections.Generic;
using UnityEngine;

public class LocationMap : MonoBehaviour
{
    [SerializeField] private List<LocationButton> _locationButtons;
    [SerializeField] private MainMenu _mainMenu;

    public int Stars
    {
        get
        {
            int stars = 0;

            foreach (var locationButton in _locationButtons)
                if (stars >= locationButton.Location.RequiredStars)
                    stars += locationButton.Location.Stars;

            return stars;
        }
    }

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

    private void OnLocationButtonClick(Location location)
    {
        _mainMenu.OpenLevels(location);
    }
}
