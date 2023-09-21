public class GeneralVolumeSlider : UISlider
{
    private Sound _sound;

    public void Init(Sound sound)
    {
        _sound = sound;
    }

    protected override void OnValueChanged(float value)
    {
        _sound.SetGeneralVolume(value);
    }
}
