public class MusicVolumeSlider : UISlider
{
    protected override void OnValueChanged(float value)
    {
        Sound.SetMusicVolume(value);
    }
}
