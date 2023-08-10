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
        _chooseButton.SetOnClickAction(() => ChooseButtonClicked?.Invoke(this));
    }

    public void Render(Weapon weapon)
    {
        base.Render(weapon.Info.Cost);
        Weapon = weapon;
        _image.sprite = weapon.Info.Sprite;
        _damageText.text = weapon.Info.Damage.ToString();
        _reloadTimeText.text = weapon.Info.SecondsBetweenShots.ToString();
    }

    public void SetChooseButtonActive(bool isActive)
    {
        _chooseButton.gameObject.SetActive(isActive);
    }
}
