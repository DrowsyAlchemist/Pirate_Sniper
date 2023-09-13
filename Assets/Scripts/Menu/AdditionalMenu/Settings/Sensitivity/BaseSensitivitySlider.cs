using UnityEngine;

public class BaseSensitivitySlider : UISlider
{
    [SerializeField] private Sensitivity _sensitivity;

    private void OnEnable()
    {
        Slider.value = _sensitivity.BaseSensitivity / Settings.Shooting.MaxSensitivity;
    }

    private void OnDisable()
    {
        _sensitivity.SetBaseSensitivity(Slider.value);
    }

    protected override void OnValueChanged(float value)
    {
    }
}
