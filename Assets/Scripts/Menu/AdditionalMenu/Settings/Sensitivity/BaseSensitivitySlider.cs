using UnityEngine;

public class BaseSensitivitySlider : UISlider
{
    [SerializeField] private Sensitivity _sensitivity;

    private void OnEnable()
    {
        Slider.value = Mathf.Pow(_sensitivity.BaseSensitivity / Settings.Shooting.MaxSensitivity, 1 / Sensitivity.ValuePower);
    }

    private void OnDisable()
    {
        _sensitivity.SetBaseSensitivity(Slider.value);
    }

    protected override void OnValueChanged(float value)
    {
    }
}
