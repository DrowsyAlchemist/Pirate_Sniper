using UnityEngine;

public class StoreMenu : Window
{
    [SerializeField] private UIButton _charactersButtton;
    [SerializeField] private UIButton _weaponsButton;

    [SerializeField] private RectTransform _waresPanel;
    [SerializeField] private RectTransform _charactersPanel;
    [SerializeField] private RectTransform _weaponsPanel;

    private void Awake()
    {
        _charactersButtton.SetOnClickAction(OpenCharacters);
        _weaponsButton.SetOnClickAction(OpenWeapons);
        _waresPanel.Activate();
        OpenCharacters();
    }

    private void OpenCharacters()
    {
        _weaponsPanel.Deactivate();
        _weaponsButton.SetInteractable(true);
        _charactersButtton.SetInteractable(false);
        _charactersPanel.Activate();
    }

    public void OpenWeapons()
    {
        _charactersPanel.Deactivate();
        _charactersButtton.SetInteractable(true);
        _weaponsButton.SetInteractable(false);
        _weaponsPanel.Activate();
    }
}
