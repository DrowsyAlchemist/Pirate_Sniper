using UnityEngine;

public class ScopeSensitivitySlider : UISlider
{
    [SerializeField] private Sensitivity _sensitivity;

    private void OnEnable()
    {
        Slider.value = _sensitivity.ScopeSensitivity / _sensitivity.BaseSensitivity;
    }

    private void OnDisable()
    {
        _sensitivity.SetScopeSensitivity(Slider.value);
    }

    protected override void OnValueChanged(float value)
    {
    }
}
