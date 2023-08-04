using UnityEngine;

[CreateAssetMenu(fileName = "ShootingSettings", menuName = "Settings/Shooting")]
public class ShootingSettings : ScriptableObject
{
    [SerializeField] private float _baseSensitivity = 200;
    [SerializeField] private float _maxSensitivity = 500;
    [SerializeField, Range(0, 1)] private float _scopeRelativeSensitivity = 0.5f;

    [SerializeField] private float _baseFieldOfView = 60;
    [SerializeField] private float _scopeFieldOfView = 35;

    [SerializeField, Range(0.01f, 1)] private float _scopeSpeed;


    public float BaseSensitivity => _baseSensitivity;
    public float ScopeSensitivity => _baseSensitivity * _scopeRelativeSensitivity;
    public float MaxSensitivity => _maxSensitivity;

    public float BaseFieldOfView => _baseFieldOfView;
    public float ScopeFieldOfView => _scopeFieldOfView;

    public float ScopeSpeed => _scopeSpeed;

    public void SetSensitivity(float value)
    {
        _baseSensitivity = Mathf.Clamp01(value) * _maxSensitivity;
    }

    public void SetScopeSensitivity(float value)
    {
        _scopeRelativeSensitivity = Mathf.Clamp01(value);
    }
}
