using UnityEngine;

public class Sensitivity : MonoBehaviour
{
    private const float ValuePower = 1.3f;
    public float _baseSensitivity = 100;
    private float _scopeRelativeSensitivity = 0.5f;
    private Saver _saver;

    public float BaseSensitivity => _baseSensitivity;
    public float ScopeSensitivity => _baseSensitivity * _scopeRelativeSensitivity;

    public void Init(Saver saver)
    {
        _saver = saver;
        _baseSensitivity = _saver.BaseSensitivity;
        _scopeRelativeSensitivity = _saver.ScopeRelativeSensitivity;
    }

    public void SetBaseSensitivity(float value)
    {
        float clampedValue = Mathf.Clamp(value, Settings.Epsilon, 1);
        _baseSensitivity = Mathf.Pow(clampedValue, ValuePower) * Settings.Shooting.MaxSensitivity;
        _saver.SetSensitivity(_baseSensitivity, _scopeRelativeSensitivity);
    }

    public void SetScopeSensitivity(float value)
    {
        _scopeRelativeSensitivity = Mathf.Clamp(value, Settings.Epsilon, 1);
        _saver.SetSensitivity(_baseSensitivity, _scopeRelativeSensitivity);
    }
}
