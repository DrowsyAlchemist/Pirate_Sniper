using UnityEngine;

public class StoreMenu : MoneyRenderer
{
    [SerializeField] private WeaponsStore _weaponsStore;

    [SerializeField] private UIButton _weaponsButton;
    [SerializeField] private UIButton _charactersButtton;

    [SerializeField] private RectTransform _waresPanel;
    [SerializeField] private RectTransform _charactersPanel;
    [SerializeField] private RectTransform _weaponsPanel;

    private void Awake()
    {
        _charactersButtton.AddOnClickAction(OpenCharacters);
        _weaponsButton.AddOnClickAction(OpenWeapons);
        _waresPanel.Activate();
    }

    private void OnEnable()
    {
        OpenWeapons();
    }

    public void Init(Wallet wallet, Saver saver, Sound sound)
    {
        base.Init(wallet);
        _weaponsStore.Init(wallet, saver, sound);
    }

    public void OpenWeapons()
    {
        _charactersPanel.Deactivate();
        _charactersButtton.SetInteractable(true);
        _weaponsButton.SetInteractable(false);
        _weaponsPanel.Activate();
        _weaponsStore.RenderWeapons();
    }

    private void OpenCharacters()
    {
        _weaponsPanel.Deactivate();
        _weaponsButton.SetInteractable(true);
        _charactersButtton.SetInteractable(false);
        _charactersPanel.Activate();
    }
}
