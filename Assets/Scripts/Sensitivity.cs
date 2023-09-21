using UnityEngine;

public class Sensitivity : MonoBehaviour
{
    public const float ValuePower = 1.3f;
    private float _baseSensitivity;
    private float _scopeRelativeSensitivity;
    private Saver _saver;

    public float BaseSensitivity => _baseSensitivity;
    public float ScopeSensitivity => _baseSensitivity * _scopeRelativeSensitivity;

    public void Init(Saver saver, bool isMobile)
    {
        _saver = saver;
        _baseSensitivity = _saver.BaseSensitivity;
        _scopeRelativeSensitivity = _saver.ScopeRelativeSensitivity;

        if (_baseSensitivity < Settings.Epsilon)
        {
            _baseSensitivity = isMobile ? Settings.Shooting.DefaultMobileBaseSensitivity : Settings.Shooting.DefaultPCBaseSensitivity;
            _scopeRelativeSensitivity = Settings.Shooting.DefaultScopeRelativeSensitivity;
            saver.SetSensitivity(_baseSensitivity, _scopeRelativeSensitivity);
        }
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
