using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarsRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _starsText;

    private const int MaxStarsPerLevel = 3;
    private int _maxStarsCount = 0;
    private IReadOnlyCollection<LocationButton> _locationButtons;

    public bool IsInitialized => _maxStarsCount > 0;
    public int CurrentStarsCount => GetCurrentStarsCount();

    private void OnEnable()
    {
        if (IsInitialized)
            Render();
    }

    public void Init(params LocationButton[] locationButtons)
    {
        _locationButtons = locationButtons;

        foreach (LocationButton locationButton in _locationButtons)
            _maxStarsCount += locationButton.Location.Levels.Count * MaxStarsPerLevel;

        Render();
    }

    public void Render()
    {
        _starsText.text = $"{GetCurrentStarsCount()}/{_maxStarsCount}";
    }

    private int GetCurrentStarsCount()
    {
        int stars = 0;

        foreach (LocationButton locationButton in _locationButtons)
            stars += locationButton.Location.Stars;

        return stars;
    }
}
