using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LocationMap _locationMap;
    [SerializeField] private AdditionalMenu _additionalMenu;

    public void Init(Player player, Saver saver)
    {
        _additionalMenu.Init(player, saver, _locationMap);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        Sound.SetBackgroundMusic(Settings.Sound.MenuMusic);
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

        gameObject.SetActive(false);
    }
}
