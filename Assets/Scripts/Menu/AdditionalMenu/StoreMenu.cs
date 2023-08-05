using UnityEngine;

public class StoreMenu : Window
{
    [SerializeField] private ShootingPoint _shootingPoint;
    [SerializeField] private Weapon _weapon;


    [SerializeField] private UIButton _charactersButtton;
    [SerializeField] private UIButton _weaponsButton;

    [SerializeField] private RectTransform _waresPanel;
    [SerializeField] private RectTransform _charactersPanel;
    [SerializeField] private RectTransform _weaponsPanel;

    private Wallet _wallet;

    private void Awake()
    {
        _charactersButtton.SetOnClickAction(OpenCharacters);
        _weaponsButton.SetOnClickAction(OpenWeapons);
        _waresPanel.Activate();
        OpenCharacters();
    }

    public void Init(Player player)
    {
        _wallet = player.Wallet;
        _shootingPoint.Init(player);
        _shootingPoint.SetWeapon(_weapon);
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
