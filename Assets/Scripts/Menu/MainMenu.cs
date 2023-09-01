using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LocationMap _locationMap;
    [SerializeField] private AdditionalMenu _additionalMenu;

    public void Init(Player player, Saver saver)
    {
        _additionalMenu.Init(player, saver, _locationMap);
        _additionalMenu.ForceClosed += OnAdditionalMenuForceClosed;
        _locationMap.Init();
        _locationMap.Deactivate();
    }

    private void OnDestroy()
    {
        _additionalMenu.ForceClosed -= OnAdditionalMenuForceClosed;
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

    public void OpenSettings()
    {
        gameObject.SetActive(true);
        _additionalMenu.Appear();
        _additionalMenu.OpenSettings();
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
