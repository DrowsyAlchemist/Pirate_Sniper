using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    public Player _player;
    private Weapon _currentWeapon;

    public void Init(Player player)
    {
        _player = player;
    }

    public void SetWeapon(Weapon weaponPrefab)
    {
        if (_currentWeapon != null)
            Destroy(_currentWeapon.gameObject);

        _currentWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
        _currentWeapon.Init();
        _player.SetWeapon(_currentWeapon);
    }
}
