using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LocationMap _locationMap;
    [SerializeField] private AdditionalMenu _additionalMenu;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OpenLevels(Location location)
    {
        _additionalMenu.Appear();
        _additionalMenu.OpenLevels(location);
    }
}
