using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Settings/Sound")]
public class SoundSettings : ScriptableObject
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _masterVolumeName = "MasterVolume";
    [SerializeField] private string _generalVolumeName = "GeneralVolume";
    [SerializeField] private string _musicVolumeName = "MusicVolume";
    [SerializeField] private string _effectsVolumeName = "EffectsVolume";
    [SerializeField] private string _uiVolumeName = "UIVolume";
    [SerializeField] private float _maxValue = -20;
    [SerializeField] private float _minValue = -80;

    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _buttleMusic;

    public AudioMixer Mixer => _mixer;
    public string MasterVolumeName => _masterVolumeName;
    public string GeneralVolumeName => _generalVolumeName;
    public string MusicVolumeName => _musicVolumeName;
    public string EffectsVolumeName => _effectsVolumeName;
    public string UIVolumeName => _uiVolumeName;
    public float MaxValue => _maxValue;
    public float MinValue => _minValue;
    public Sprite OnSprite => _onSprite;
    public Sprite OffSprite => _offSprite;
    public AudioClip MenuMusic => _menuMusic;
    public AudioClip ButtleMusic => _buttleMusic;
}
