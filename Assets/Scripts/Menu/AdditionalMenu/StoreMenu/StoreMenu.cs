using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MoneyRenderer
{
    [SerializeField] private ShootingPoint _shootingPoint;

    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private WeaponRenderer _weaponWareTemplate;
    [SerializeField] private RectTransform _weaponsContainer;

    [SerializeField] private UIButton _weaponsButton;
    [SerializeField] private UIButton _charactersButtton;

    [SerializeField] private RectTransform _waresPanel;
    [SerializeField] private RectTransform _charactersPanel;
    [SerializeField] private RectTransform _weaponsPanel;

    private List<WeaponRenderer> _weaponWares = new();
    private Wallet _wallet;
    private Saver _saver;

    public Weapon CurrentWeapon
    {
        get
        {
            foreach (var weapon in _weapons)
                if (weapon.Info.Id.Equals(_saver.GetCurrentWeapon()))
                    return weapon;

            throw new InvalidOperationException();
        }
    }

    private void Awake()
    {
        _charactersButtton.SetOnClickAction(OpenCharacters);
        _weaponsButton.SetOnClickAction(OpenWeapons);
        _waresPanel.Activate();
        OpenWeapons();
    }

    private void OnDestroy()
    {
        foreach (var weaponWare in _weaponWares)
        {
            weaponWare.BuyButtonClicked -= OnWeaponBuyButtonClick;
            weaponWare.ChooseButtonClicked -= OnWeaponChooseButtonClick;
        }
    }

    private void OnEnable()
    {
        RenderWeapons();
    }

    public void Init(Wallet wallet, Saver saver)
    {
        base.Init(wallet);
        _wallet = wallet;
        _saver = saver;
    }

    private void OpenCharacters()
    {
       // _weaponsPanel.Deactivate();
       // _weaponsButton.SetInteractable(true);
       // _charactersButtton.SetInteractable(false);
       // _charactersPanel.Activate();
    }

    public void OpenWeapons()
    {
        _charactersPanel.Deactivate();
        _charactersButtton.SetInteractable(true);
        _weaponsButton.SetInteractable(false);
        _weaponsPanel.Activate();
    }

    private void RenderWeapons()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weaponWares.Count <= i)
            {
                WeaponRenderer weaponWare = Instantiate(_weaponWareTemplate, _weaponsContainer);
                weaponWare.BuyButtonClicked += OnWeaponBuyButtonClick;
                weaponWare.ChooseButtonClicked += OnWeaponChooseButtonClick;
                _weaponWares.Add(weaponWare);
            }
            _weaponWares[i].Render(_weapons[i]);

            if (_saver.GetWeaponAccuired(_weapons[i]))
            {
                _weaponWares[i].DeactivateBuyButton();
                bool isCurrentWeapon = _saver.GetCurrentWeapon().Equals(_weapons[i].Info.Id);
                _weaponWares[i].SetChooseButtonActive(isCurrentWeapon == false);
            }
            else
            {
                _weaponWares[i].SetChooseButtonActive(false);
            }
        }
    }

    private void OnWeaponBuyButtonClick(WareRenderer wareRenderer)
    {
        var weaponRenderer = wareRenderer as WeaponRenderer;

        if (weaponRenderer.Weapon.Info.Cost <= 0)
            Advertising.RewardForAd(() => AccuireWeapon(weaponRenderer));
        else if (_wallet.TryGiveMoney(weaponRenderer.Weapon.Info.Cost))
            AccuireWeapon(weaponRenderer);
    }

    private void OnWeaponChooseButtonClick(WeaponRenderer weaponRenderer)
    {
        ChooseWeapon(weaponRenderer);
        weaponRenderer.SetChooseButtonActive(false);
    }

    private void AccuireWeapon(WeaponRenderer weaponRenderer)
    {
        _saver.SaveWeaponAccuired(weaponRenderer.Weapon);
        weaponRenderer.DeactivateBuyButton();
        ChooseWeapon(weaponRenderer);
    }

    private void ChooseWeapon(WeaponRenderer weaponRenderer)
    {
        _shootingPoint.SetWeapon(weaponRenderer.Weapon);
        RenderWeapons();
    }
}
