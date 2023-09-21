using UnityEngine;

public class BaseSensitivitySlider : UISlider
{
    private Sensitivity _sensitivity;

    private void OnEnable()
    {
        Slider.value = Mathf.Pow(_sensitivity.BaseSensitivity / Settings.Shooting.MaxSensitivity, 1 / Sensitivity.ValuePower);
    }

    private void OnDisable()
    {
        _sensitivity.SetBaseSensitivity(Slider.value);
    }

    public void Init(Sensitivity sensitivity)
    {
        _sensitivity = sensitivity;
    }

    protected override void OnValueChanged(float value)
    {
    }
}
