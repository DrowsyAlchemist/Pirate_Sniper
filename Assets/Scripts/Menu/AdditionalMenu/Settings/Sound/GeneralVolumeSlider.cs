public class GeneralVolumeSlider : UISlider
{
    private void OnEnable()
    {
        //Slider.value = Sound.GetNormalizedVolume(Settings.Sound.MasterVolumeName);
    }

    protected override void OnValueChanged(float value)
    {
        Sound.SetGeneralVolume(value);
    }
}
