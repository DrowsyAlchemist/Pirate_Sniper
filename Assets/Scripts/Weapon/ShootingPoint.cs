using System;
using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    public Player _player;
    private Weapon _currentWeapon;

    public Weapon CurrentWeapon => _currentWeapon;

    public event Action Shooted;

    public void Init(Player player)
    {
        _player = player;
    }

    public void SetWeapon(Weapon weaponPrefab)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Shooted -= OnShooted;
            Destroy(_currentWeapon.gameObject);
        }
        _currentWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
        _currentWeapon.Init();
        _currentWeapon.Shooted += OnShooted;
        _player.SetWeapon(_currentWeapon);
    }

    private void OnShooted()
    {
        Shooted?.Invoke();
    }
}
