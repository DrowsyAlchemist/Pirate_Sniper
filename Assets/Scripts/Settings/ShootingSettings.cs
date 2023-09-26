using UnityEngine;

[CreateAssetMenu(fileName = "ShootingSettings", menuName = "Settings/Shooting")]
public class ShootingSettings : ScriptableObject
{
    [SerializeField] private Weapon _defaultWeapon;
    [SerializeField] private float _maxSensitivity = 200;
    [SerializeField] private float _baseFieldOfView = 60;
    [SerializeField] private float _scopeFieldOfView = 35;
    [SerializeField, Range(0.01f, 1)] private float _scopeSpeed;

    [SerializeField] private float _pcSensitivityModifier = 6;
    [SerializeField] private float _defaultBaseSensitivity = 10;
    [SerializeField] private float _defaultScopeRelativeSensitivity = 0.25f;

    public Weapon DefaultWeapon => _defaultWeapon;
    public float MaxSensitivity => _maxSensitivity;
    public float BaseFieldOfView => _baseFieldOfView;
    public float ScopeFieldOfView => _scopeFieldOfView;
    public float ScopeSpeed => _scopeSpeed;

    public float PCSensitivityModifier => _pcSensitivityModifier;
    public float DefaultBaseSensitivity => _defaultBaseSensitivity;
    public float DefaultScopeRelativeSensitivity => _defaultScopeRelativeSensitivity;
}
