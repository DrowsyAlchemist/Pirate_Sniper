public class ScopeSensitivitySlider : UISlider
{
    private Sensitivity _sensitivity;

    private void OnEnable()
    {
        Slider.value = _sensitivity.ScopeSensitivity / _sensitivity.BaseSensitivity;
    }

    private void OnDisable()
    {
        _sensitivity.SetScopeSensitivity(Slider.value);
    }

    public void Init(Sensitivity sensitivity)
    {
        _sensitivity = sensitivity;
    }

    protected override void OnValueChanged(float value)
    {
    }
}
