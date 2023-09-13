public class GeneralVolumeSlider : UISlider
{
    protected override void OnValueChanged(float value)
    {
        Sound.SetGeneralVolume(value);
    }
}
