using UnityEngine;

[CreateAssetMenu(fileName = "ShootingSettings", menuName = "Settings/Shooting")]
public class ShootingSettings : ScriptableObject
{
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private float _maxSensitivity = 200;

    [SerializeField] private float _baseFieldOfView = 60;
    [SerializeField] private float _scopeFieldOfView = 35;

    [SerializeField, Range(0.01f, 1)] private float _scopeSpeed;

    private const float ValuePower = 1.3f;
    private float _baseSensitivity = 100;
    private float _scopeRelativeSensitivity = 0.5f;

    public Weapon DefaultWeapon => _defaultWeapon;
    public float BaseSensitivity => _baseSensitivity;
    public float ScopeSensitivity => _baseSensitivity * _scopeRelativeSensitivity;
    public float MaxSensitivity => _maxSensitivity;

    public float BaseFieldOfView => _baseFieldOfView;
    public float ScopeFieldOfView => _scopeFieldOfView;

    public float ScopeSpeed => _scopeSpeed;

    public void SetSensitivity(float value)
    {
        float clampedValue = Mathf.Clamp(value, Settings.Epsilon, 1);
        _baseSensitivity = Mathf.Pow(clampedValue, ValuePower) * _maxSensitivity;
    }

    public void SetScopeSensitivity(float value)
    {
        _scopeRelativeSensitivity = Mathf.Clamp(value, Settings.Epsilon, 1);
    }
}
