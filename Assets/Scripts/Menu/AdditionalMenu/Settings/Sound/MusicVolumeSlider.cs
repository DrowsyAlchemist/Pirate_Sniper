public class MusicVolumeSlider : UISlider
{
    private void OnEnable()
    {
        //Slider.value = Sound.GetNormalizedVolume(Settings.Sound.MusicVolumeName);
    }

    protected override void OnValueChanged(float value)
    {
        Sound.SetMusicVolume(value);
    }
}
