using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnEnable()
    {
        _slider.value = Settings.ShootingSettings.BaseSensitivity / Settings.ShootingSettings.MaxSensitivity;
    }

    private void OnValueChanged(float value)
    {
        Settings.ShootingSettings.SetSensitivity(value * Settings.ShootingSettings.MaxSensitivity);
    }
}
