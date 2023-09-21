using UnityEngine;

public class SettingsMenu : Window
{
    [SerializeField] private SoundOnOffButton _soundOnOffButton;
    [SerializeField] private GeneralVolumeSlider _masterVolume;
    [SerializeField] private MusicVolumeSlider _musicVolume;
    [SerializeField] private BaseSensitivitySlider _baseSensitivity;
    [SerializeField] private ScopeSensitivitySlider _scopeSensitivity;

    [SerializeField] private Sensitivity _sensitivity;

    private Sound _sound;

    public void Init(Sound sound)
    {
        _sound = sound;
        _soundOnOffButton.Init(sound);
        _masterVolume.Init(_sound);
        _musicVolume.Init(sound);
        _baseSensitivity.Init(_sensitivity);
        _scopeSensitivity.Init(_sensitivity);
    }


}
