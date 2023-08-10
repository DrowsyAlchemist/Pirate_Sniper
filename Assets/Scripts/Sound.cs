using UnityEngine;
using Agava.WebUtility;
using System;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _buySound;

    private static Sound _instance;

    public static bool IsOn { get; private set; }
    public static AudioSource BackgroundMusic => _instance._backgroundMusic;
    protected static SoundSettings SoundSettings => Settings.Sound;

    public static event Action<bool> ConditionChanged;

    private void Awake()
    {
        if (_instance == false)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        TurnOn();
        WebApplication.InBackgroundChangeEvent += OnBackgroundChanged;
        ConditionChanged?.Invoke(false);
    }

    private void OnDestroy()
    {
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChanged;
    }

    public static float GetNormalizedVolume(string volumeName)
    {
        SoundSettings.Mixer.GetFloat(volumeName, out float value);
        return Mathf.Lerp(SoundSettings.MinValue, SoundSettings.MaxValue, value);
    }

    public static void TurnOn()
    {
        _instance.TurnSoundOn();
        IsOn = true;
        ConditionChanged?.Invoke(true);
    }

    public static void Mute()
    {
        _instance.TurnSoundOff();
        IsOn = false;
        ConditionChanged?.Invoke(false);
    }

    public static void SetBackgroundMusic(AudioClip clip)
    {
        _instance._backgroundMusic.clip = clip;
        _instance._backgroundMusic.Play();
    }

    public static void SetGeneralVolume(float normalizedValue)
    {
        SetVolume(SoundSettings.GeneralVolumeName, normalizedValue);
    }

    public static void SetMusicVolume(float normalizedValue)
    {
        SetVolume(SoundSettings.MusicVolumeName, normalizedValue);
    }

    public static void SetEffectsVolume(float normalizedValue)
    {
        SetVolume(SoundSettings.EffectsVolumeName, normalizedValue);
    }

    public static void SetUIVolume(float normalizedValue)
    {
        SetVolume(SoundSettings.UIVolumeName, normalizedValue);
    }

    public static void PlayClick()
    {
        _instance._clickSound.Play();
    }

    public static void PlayBuy()
    {
        _instance._buySound.Play();
    }

    private void OnBackgroundChanged(bool isOut)
    {
        if (isOut)
            TurnSoundOff();
        else if (IsOn)
            TurnSoundOn();
    }

    private void TurnSoundOn()
    {
        SetVolume(SoundSettings.MasterVolumeName, 1);
    }

    private void TurnSoundOff()
    {
        SetVolume(SoundSettings.MasterVolumeName, 0);
    }

    private static void SetVolume(string volumeName, float normalizedValue)
    {
        SoundSettings.Mixer.SetFloat(volumeName, Mathf.Lerp(SoundSettings.MinValue, SoundSettings.MaxValue, normalizedValue));
    }
}