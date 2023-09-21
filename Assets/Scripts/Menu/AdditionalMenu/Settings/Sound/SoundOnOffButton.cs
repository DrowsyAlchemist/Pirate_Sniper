using UnityEngine;
using UnityEngine.UI;

public class SoundOnOffButton : UIButton
{
    [SerializeField] private Image _image;

    private Sound _sound;

    public void Init(Sound sound)
    {
        _sound = sound;
        base.AddOnClickAction(OnButtonClick);

        if (_sound.IsOn)
            OnSoundTurnedOn();
        else
            OnSoundTurnedOff();

        _sound.TurnedOn += OnSoundTurnedOn;
        _sound.TurnedOff += OnSoundTurnedOff;
    }

    protected override void OnButtonDestroy()
    {
        base.OnButtonDestroy();
        _sound.TurnedOn -= OnSoundTurnedOn;
        _sound.TurnedOff -= OnSoundTurnedOff;
    }

    private void OnButtonClick()
    {
        if (_sound.IsOn)
        {
            _sound.Mute();
            _image.sprite = Settings.Sound.OffSprite;
        }
        else
        {
            _sound.TurnOn();
            _image.sprite = Settings.Sound.OnSprite;
        }
    }

    private void OnSoundTurnedOn()
    {
        _image.sprite = Settings.Sound.OnSprite;
    }

    private void OnSoundTurnedOff()
    {
        _image.sprite = Settings.Sound.OffSprite;
    }
}