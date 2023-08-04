public class BaseSensitivitySlider : UISlider
{
    private void OnEnable()
    {
        Slider.value = Settings.Shooting.BaseSensitivity / Settings.Shooting.MaxSensitivity;
    }

    protected override void OnValueChanged(float value)
    {
        Settings.Shooting.SetSensitivity(value);
    }
}
