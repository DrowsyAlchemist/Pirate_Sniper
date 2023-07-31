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
        _slider.value = Settings.Shooting.BaseSensitivity / Settings.Shooting.MaxSensitivity;
    }

    private void OnValueChanged(float value)
    {
        Settings.Shooting.SetSensitivity(value * Settings.Shooting.MaxSensitivity);
    }
}
