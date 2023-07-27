using UnityEngine;

public class AdditionalMenu : Window
{
    [SerializeField] private LevelsMenu _levelsMenu;

    public void OpenLevels(Location location)
    {
        _levelsMenu.Activate();
        _levelsMenu.RenderLocationLevels(location);
    }

    public void OpenCharacteristics()
    {

    }

    public void OpenStore()
    {

    }

    public void OpenLeaderboard()
    {

    }

    public void OpenSettings()
    {

    }
}
