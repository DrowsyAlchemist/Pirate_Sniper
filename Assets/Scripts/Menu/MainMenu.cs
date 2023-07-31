using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LocationMap _locationMap;
    [SerializeField] private AdditionalMenu _additionalMenu;

    public void Init(Player player)
    {
        _additionalMenu.Init(player, _locationMap);
    }

    public void Open()
    {
        gameObject.SetActive(true);
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
