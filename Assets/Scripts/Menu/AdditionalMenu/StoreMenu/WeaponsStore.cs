using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsStore : MonoBehaviour
{
    [SerializeField] private ShootingPoint _shootingPoint;
    [SerializeField] private WeaponsStorage _weaponStorage;
    [SerializeField] private WeaponRenderer _weaponWareTemplate;
    [SerializeField] private RectTransform _container;

    private List<WeaponRenderer> _wares = new();

    private Wallet _wallet;
    private Saver _saver;

    public IReadOnlyList<Weapon> Weapons => _weaponStorage.Weapons;

    public Weapon CurrentWeapon
    {
        get
        {
            foreach (var weapon in Weapons)
                if (weapon.Id.Equals(_saver.GetCurrentWeapon()))
                    return weapon;

            throw new InvalidOperationException();
        }
    }

    private void OnDestroy()
    {
        foreach (var weaponWare in _wares)
        {
            weaponWare.BuyButtonClicked -= OnBuyButtonClick;
            weaponWare.ChooseButtonClicked -= OnChooseButtonClick;
        }
    }

    public void Init(Wallet wallet, Saver saver)
    {
        _wallet = wallet;
        _saver = saver;
    }

    public void RenderWeapons()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            if (_wares.Count <= i)
            {
                WeaponRenderer weaponWare = Instantiate(_weaponWareTemplate, _container);
                weaponWare.BuyButtonClicked += OnBuyButtonClick;
                weaponWare.ChooseButtonClicked += OnChooseButtonClick;
                _wares.Add(weaponWare);
            }
            _wares[i].Render(Weapons[i]);

            if (_saver.GetWeaponAccuired(Weapons[i]))
            {
                _wares[i].DeactivateBuyButton();
                bool isCurrentWeapon = _saver.GetCurrentWeapon().Equals(Weapons[i].Id);
                _wares[i].SetChooseButtonActive(isCurrentWeapon == false);
            }
            else
            {
                _wares[i].SetChooseButtonActive(false);
            }
        }
    }

    private void OnBuyButtonClick(WareRenderer wareRenderer)
    {
        var weaponRenderer = wareRenderer as WeaponRenderer;

        if (weaponRenderer.Weapon.Cost <= 0)
            Advertising.RewardForVideo(() => AccuireWeapon(weaponRenderer));
        else if (_wallet.TryGiveMoney(weaponRenderer.Weapon.Cost))
            AccuireWeapon(weaponRenderer);
    }

    private void OnChooseButtonClick(WeaponRenderer weaponRenderer)
    {
        Sound.PlayClick();
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
