using UnityEngine;
using Agava.WebUtility;
using System;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private AudioSource _clickSound;
    [SerializeField] private AudioSource _buySound;

    private const float ValuePower = 0.3f;
    private const float MasterVolumeModifier = 0.5f;

    public bool IsOn { get; private set; }
    public AudioSource BuySound => _buySound;
    protected SoundSettings SoundSettings => Settings.Sound;

    public event Action TurnedOn;
    public event Action TurnedOff;

    public void Init()
    {
        TurnOn();
        WebApplication.InBackgroundChangeEvent += OnBackgroundChanged;
    }

    private void OnDestroy()
    {
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChanged;
    }

    public void TurnOn()
    {
        TurnSoundOn();
        IsOn = true;
        TurnedOn?.Invoke();
    }

    public void Mute()
    {
        TurnSoundOff();
        IsOn = false;
        TurnedOff?.Invoke();
    }

    public void SetBackgroundMusic(AudioClip clip)
    {
        _backgroundMusic.clip = clip;
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (Advertising.IsRunning == false)
            _backgroundMusic.Play();
    }

    public void StopBackgroundMusic()
    {
        _backgroundMusic.Stop();
    }

    public void SetGeneralVolume(float normalizedValue)
    {
        SetVolume(SoundSettings.GeneralVolumeName, normalizedValue);
    }

    public void SetMusicVolume(float normalizedValue)
    {
        SetVolume(SoundSettings.MusicVolumeName, normalizedValue);
    }

    public void SetEffectsVolume(float normalizedValue)
    {
        SetVolume(SoundSettings.EffectsVolumeName, normalizedValue);
    }

    public void SetUIVolume(float normalizedValue)
    {
        SetVolume(SoundSettings.UIVolumeName, normalizedValue);
    }

    public void PlayClick()
    {
        _clickSound.Play();
    }

    public void PlayBuy()
    {
        _buySound.Play();
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
        SetVolume(SoundSettings.MasterVolumeName, MasterVolumeModifier);
    }

    private void TurnSoundOff()
    {
        SetVolume(SoundSettings.MasterVolumeName, 0);
    }

    private void SetVolume(string volumeName, float normalizedValue)
    {
        float poweredValue = Mathf.Pow(normalizedValue, ValuePower);
        SoundSettings.Mixer.SetFloat(volumeName, Mathf.Lerp(SoundSettings.MinValue, SoundSettings.MaxValue, poweredValue));
    }
}