using System;
using System.Collections;
using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    [SerializeField] private float _unscopeXPosition;
    [SerializeField] private float _scopeXPosition;
    [SerializeField, Range(0, 0.5f)] private float _positionChangeSpeed;
    [SerializeField] private WeaponsStore _weaponsStore;
    [SerializeField] private AudioSource _shootSound;

    public Player _player;
    private Weapon _currentWeapon;
    private InputController _inputController;
    private Coroutine _coroutine;

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
        _currentWeapon = Instantiate(weaponPrefab, transform.position + weaponPrefab.transform.localPosition, Quaternion.identity, transform);
        _currentWeapon.Init();
        _currentWeapon.Shooted += OnShooted;
        _player.SetWeapon(_currentWeapon);
    }

    private void OnShooted()
    {
        _shootSound.clip = _currentWeapon.ShootClip;
        _shootSound.Play();
        Shooted?.Invoke();
    }

    private void OnScope()
    {
        if (_coroutine != null)
            Settings.CoroutineObject.StopCoroutine(_coroutine);

        _coroutine = Settings.CoroutineObject.StartCoroutine(ChangeXPosition(_scopeXPosition));
    }

    private void OnUnscope()
    {
        if (_coroutine != null)
            Settings.CoroutineObject.StopCoroutine(_coroutine);

        _coroutine = Settings.CoroutineObject.StartCoroutine(ChangeXPosition(_unscopeXPosition));
    }

    private IEnumerator ChangeXPosition(float targetPosition)
    {
        while (Mathf.Abs(transform.localPosition.x - targetPosition) > Settings.Epsilon)
        {
            float targetX = Mathf.Lerp(transform.localPosition.x, targetPosition, _positionChangeSpeed);
            transform.localPosition = new Vector3(targetX, transform.localPosition.y, transform.localPosition.z);
            yield return new WaitForFixedUpdate();
        }
    }
}
