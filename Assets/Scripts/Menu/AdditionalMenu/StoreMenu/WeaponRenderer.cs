using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponRenderer : WareRenderer
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _reloadTimeText;

    [SerializeField] private UIButton _chooseButton;

    public Weapon Weapon { get; private set; }

    public event Action<WeaponRenderer> ChooseButtonClicked;

    private void Start()
    {
        _chooseButton.AddOnClickAction(() => ChooseButtonClicked?.Invoke(this));
    }

    public void Render(Weapon weapon, Wallet wallet)
    {
        base.Render(weapon.Cost, wallet);
        Weapon = weapon;
        _image.sprite = weapon.Sprite;
        _damageText.text = weapon.Damage.ToString();
        _reloadTimeText.text = weapon.SecondsBetweenShots.ToString();
    }

    public void SetChooseButtonActive(bool isActive)
    {
        _chooseButton.gameObject.SetActive(isActive);
    }
}
