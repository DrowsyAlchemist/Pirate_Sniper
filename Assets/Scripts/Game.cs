using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private bool _useMobileControlInEditor;
    [SerializeField] private PointerMoveArea _pointerMoveArea;
    [SerializeField] private ShootingButton _pcShootingButton;
    [SerializeField] private ShootingButton _mobileShootingButton;

    [SerializeField] private OpenMenuButton _menuButton;
    [SerializeField] private Camera _camera;

    private static Game _instance;
    private InputController _inputController;

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
#if UNITY_EDITOR
        bool isMobile = _useMobileControlInEditor;
#else
        bool isMobile = Device.IsMobile; 
#endif
        _mobileShootingButton.gameObject.SetActive(isMobile);
        var shootingButton = isMobile ? _mobileShootingButton : _pcShootingButton;
        _inputController = new InputController(_camera, _pointerMoveArea, shootingButton);
        _menuButton.Init(isMobile);
    }
}
