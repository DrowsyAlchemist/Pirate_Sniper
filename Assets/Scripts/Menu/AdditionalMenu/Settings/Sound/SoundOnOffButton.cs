using UnityEngine;
using UnityEngine.UI;

public class SoundOnOffButton : UIButton
{
    [SerializeField] private Image _image;

    private void Start()
    {
        base.AddOnClickAction(OnButtonClick);
        OnSoundConditionChanged(Sound.IsOn);
        Sound.ConditionChanged += OnSoundConditionChanged;
    }

    protected override void OnButtonDestroy()
    {
        base.OnButtonDestroy();
        Sound.ConditionChanged -= OnSoundConditionChanged;
    }

    private void OnButtonClick()
    {
        if (Sound.IsOn)
        {
            Sound.Mute();
            _image.sprite = Settings.Sound.OffSprite;
        }
        else
        {
            Sound.TurnOn();
            _image.sprite = Settings.Sound.OnSprite;
        }
    }

    private void OnSoundConditionChanged(bool isOn)
    {
        if (isOn)
            _image.sprite = Settings.Sound.OnSprite;
        else
            _image.sprite = Settings.Sound.OffSprite;
    }
}