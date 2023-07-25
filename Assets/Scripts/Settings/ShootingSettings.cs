using UnityEngine;

[CreateAssetMenu(fileName = "ShootingSettings", menuName = "Settings/Shooting")]
public class ShootingSettings : ScriptableObject
{
    [SerializeField] private float _baseSensitivity = 200;
    [SerializeField] private float _baseFieldOfView = 60;

    [SerializeField, Range(0, 1)] private float _scopeRelativeSensitivity = 0.5f;
    [SerializeField] private float _scopeFieldOfView = 35;

    public float BaseSensitivity => _baseSensitivity;
    public float BaseFieldOfView => _baseFieldOfView;
    public float ScopeSensitivity => _baseSensitivity * _scopeRelativeSensitivity;
    public float ScopeFieldOfView => _scopeFieldOfView;
}
