using System;
using System.Collections;
using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    [SerializeField] private float _initialXPosition;
    [SerializeField] private float _scopeXPosition;
    [SerializeField, Range(0, 0.1f)] private float _positionChangeSpeed;
    [SerializeField] private StoreMenu _weaponsStore;

    public Player _player;
    private Weapon _currentWeapon;
    private InputController _inputController;

    public Weapon CurrentWeapon => _currentWeapon;

    public event Action Shooted;

    public void Init(Player player, InputController inputController)
    {
        _player = player;
        _inputController = inputController;
        _inputController.Scoped += OnScope;
        _inputController.Unscoped += OnUnscope;
        SetWeapon(_weaponsStore.CurrentWeapon);
    }

    private void OnDestroy()
    {
        _inputController.Scoped -= OnScope;
        _inputController.Unscoped -= OnUnscope;
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

    private void OnScope()
    {
        Settings.CoroutineObject.StartCoroutine(ChangeXPosition(_scopeXPosition));
    }

    private void OnUnscope()
    {
        Settings.CoroutineObject.StartCoroutine(ChangeXPosition(_initialXPosition));
    }

    private IEnumerator ChangeXPosition(float targetPosition)
    {
        while (Mathf.Abs(transform.position.x - targetPosition) > Settings.Epsilon)
        {
            float targetX = Mathf.Lerp(transform.position.x, targetPosition, _positionChangeSpeed);
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
            yield return null;
        }
    }
}
