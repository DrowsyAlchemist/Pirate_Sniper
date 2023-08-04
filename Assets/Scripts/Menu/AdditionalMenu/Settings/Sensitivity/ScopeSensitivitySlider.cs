public class ScopeSensitivitySlider : UISlider
{
    private void OnEnable()
    {
        Slider.value = Settings.Shooting.ScopeSensitivity / Settings.Shooting.BaseSensitivity;
    }

    protected override void OnValueChanged(float value)
    {
        Settings.Shooting.SetScopeSensitivity(value);
    }
}
