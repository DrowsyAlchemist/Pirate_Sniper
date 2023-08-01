using Agava.WebUtility;
using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private bool _useMobileControlInEditor;
    [SerializeField] private InputController _inputController;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Level _level;

    private Player _player;
    private Saver _saver;

    private IEnumerator Start()
    {
#if UNITY_EDITOR
        Init();
        yield break;
#endif
        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();

        Init();
    }

    private void Init()
    {
        InitInputController();
        _saver = new Saver();
        _player = new Player(_inputController, _saver);
        _mainMenu.Init(_player);
        _level.Init(_player, _saver);
    }

    public void RemoveSaves()
    {
        _saver.RemoveSaves();
    }

    private void InitInputController()
    {
        bool isMobile;
#if UNITY_EDITOR
        isMobile = _useMobileControlInEditor;
        _inputController.Init(isMobile);
        return;
#endif
        isMobile = Device.IsMobile;
        _inputController.Init(isMobile);
    }
}
