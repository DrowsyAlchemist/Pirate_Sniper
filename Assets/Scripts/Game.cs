using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private bool _useMobileControlInEditor;
    [SerializeField] private InputController _inputController;
    [SerializeField] private OpenMenuButton _menuButton;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private LevelsMenu _levelsMenu;

    private static Game _instance;
    private Player _player;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

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
        _player = new Player(_inputController, 100, 50);
        _levelsMenu.LevelClicked += LoadLevel;
    }

    private void InitInputController()
    {
#if UNITY_EDITOR
        bool isMobile = _useMobileControlInEditor;
#else
        bool isMobile = Device.IsMobile; 
#endif
        _inputController.Init(isMobile);
        _menuButton.Init(isMobile);
    }

    private void LoadLevel(Level levelTemplate)
    {
        var level = Instantiate(levelTemplate);
        level.Init();
        _mainMenu.Close();
    }
}
