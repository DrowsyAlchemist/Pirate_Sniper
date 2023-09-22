using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LocationMap _locationMap;
    [SerializeField] private AdditionalMenu _additionalMenu;
    
    private Sound _sound;

    public void Init(Player player, Saver saver, Sound sound)
    {
        _sound = sound; GameInitialization.ErrorText.text += " MainMenu 13";
        _additionalMenu.Init(player, saver, _locationMap, _sound); GameInitialization.ErrorText.text += " MainMenu 14";
        _locationMap.Init(); GameInitialization.ErrorText.text += " MainMenu 15";
        _locationMap.Deactivate(); GameInitialization.ErrorText.text += " MainMenu 16";
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _sound.SetBackgroundMusic(Settings.Sound.MenuMusic);
    }

    public void Close()
    {
        if (gameObject.activeSelf)
            StartCoroutine(CloseWithDelay());
    }

    private IEnumerator CloseWithDelay()
    {
        _additionalMenu.Disappear();

        while (_additionalMenu.IsPlaying)
            yield return null;

        _locationMap.Deactivate();
        gameObject.SetActive(false);
    }

    private void OnAdditionalMenuForceClosed()
    {
        gameObject.SetActive(false);
    }
}
