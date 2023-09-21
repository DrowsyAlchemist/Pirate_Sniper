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
    [SerializeField] private AudioSource _misfireSound;
    [SerializeField] private AudioSource _reloadingFinishedSound;
    [SerializeField] private HitEffect _hitEffect;

    private Saver _saver;
    private Player _player;
    private Weapon _currentWeapon;
    private InputHandler _inputHandler;
    private Coroutine _coroutine;

    public Weapon CurrentWeapon => _currentWeapon;

    public event Action Shooted;

    public void Init(Saver saver, InputHandler inputHandler, Player player)
    {
        _saver = saver;
        _player = player;
        _inputHandler = inputHandler;
        _inputHandler.Scoped += OnScope;
        _inputHandler.Unscoped += OnUnscope;
        _inputHandler.Shooted += OnTryShooting;
        SetWeapon(_weaponsStore.CurrentWeapon);
        _hitEffect.Init();
    }

    private void OnDestroy()
    {
        _inputHandler.Scoped -= OnScope;
        _inputHandler.Unscoped -= OnUnscope;
        _inputHandler.Shooted -= OnTryShooting;
    }

    public void SetWeapon(Weapon weaponPrefab)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.ReloadingFinished -= OnWeaponReloadingFinished;
            Destroy(_currentWeapon.gameObject);
        }
        _currentWeapon = Instantiate(weaponPrefab, transform.position + weaponPrefab.transform.localPosition, transform.rotation, transform);
        _currentWeapon.Init(_hitEffect);
        _currentWeapon.ReloadingFinished += OnWeaponReloadingFinished;
        _saver.SetCurrentWeapon(_currentWeapon);
        _shootSound.clip = _currentWeapon.ShootClip;
    }

    private void OnWeaponReloadingFinished()
    {
        if (_inputHandler.InputMode == InputMode.Game)
            _reloadingFinishedSound.Play();
    }

    private void OnTryShooting(RaycastHit raycastHit)
    {
        bool isGameReady = _inputHandler.InputMode == InputMode.Game;
        bool isPlayerReady = _player.Health.IsAlive;
        bool isWeaponReady = _currentWeapon.IsReady;

        if (isGameReady == false || isPlayerReady == false)
            return;

        if (isWeaponReady)
        {
            _currentWeapon.Shoot(raycastHit,_player.Damage);
            _shootSound.Play();
            Shooted?.Invoke();
        }
        else
        {
            _misfireSound.Play();
        }
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
