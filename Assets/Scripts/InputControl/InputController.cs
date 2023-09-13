using System;
using System.Collections;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PointerMoveArea _pointerMoveArea;
    [SerializeField] private PointerDownArea _pointerDownArea;
    [SerializeField] private PointerUpArea _pointerUpArea;

    [SerializeField] private PauseButton _pauseButton;

    [SerializeField] private Level _level;

    private const float DefaultZRotation = 0;
    private const float AngleEpsilon = 50;
    private static InputController _instance;
    private Sensitivity _sensitivity;
    private bool _isScopeMode;
    private Coroutine _coroutine;

    private float _xUpperBound;
    private float _xLowerBound;
    private float _yUpperBound;
    private float _yLowerBound;

    public event Action Scoped;
    public event Action Unscoped;
    public event Action<RaycastHit> Shooted;

    public static bool IsMobile { get; private set; }
    public static InputMode InputMode { get; private set; }
    public float CurrentSensitivity => _isScopeMode ? _sensitivity.ScopeSensitivity : _sensitivity.BaseSensitivity;
    public float ZeroXRotation => _level.CurrentLevel.CameraTransform.rotation.x;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _pointerMoveArea.PointerMove -= OnPointerMove;
        _pointerDownArea.PointerDown -= Scope;
        _pointerUpArea.PointerUp -= Shoot;
        _level.LevelLoaded -= OnNewLevelLoaded;
    }

    public static void SetMode(InputMode mode)
    {
        InputMode = mode;

        if (mode == InputMode.UI)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _instance._pointerMoveArea.enabled = false;
            _instance._pointerUpArea.enabled = false;
            _instance._pointerDownArea.enabled = false;
        }
        else
        {
            if (IsMobile == false)
                Cursor.lockState = CursorLockMode.Locked;

            _instance._pointerMoveArea.enabled = true;
            _instance._pointerUpArea.enabled = true;
            _instance._pointerDownArea.enabled = true;
            _instance.Unscope();
        }
    }

    public void Init(bool isMobile, Sensitivity sensitivity)
    {
        IsMobile = isMobile;
        _sensitivity = sensitivity;
        SetMode(InputMode.UI);
        _pauseButton.Init(isMobile);
        _pointerDownArea.Init();
        _pointerDownArea.PointerDown += Scope;
        _pointerUpArea.PointerUp += Shoot;
        _pointerMoveArea.PointerMove += OnPointerMove;
        _level.LevelLoaded += OnNewLevelLoaded;
    }

    public void OnPointerMove()
    {
        Vector3 cameraAngles = _camera.transform.localRotation.eulerAngles;

        float targetXAngle = cameraAngles.x + Time.deltaTime * CurrentSensitivity * -1 * Input.GetAxis("Mouse Y");
        float xAngleToCheck = targetXAngle % 360;

        if ((xAngleToCheck - _xLowerBound) < 0 && (xAngleToCheck - _xLowerBound) > -1 * AngleEpsilon
            || (xAngleToCheck - _xUpperBound) > 0 && (xAngleToCheck - _xUpperBound) < AngleEpsilon)
            targetXAngle = cameraAngles.x;

        float targetYAngle = cameraAngles.y + Time.deltaTime * CurrentSensitivity * Input.GetAxis("Mouse X");
        float yAngleToCheck = targetYAngle % 360;

        if ((yAngleToCheck - _yLowerBound) < 0 && (yAngleToCheck - _yLowerBound) > -1 * AngleEpsilon
            || (yAngleToCheck - _yUpperBound) > 0 && (yAngleToCheck - _yUpperBound) < AngleEpsilon)
            targetYAngle = cameraAngles.y;

        _camera.transform.SetPositionAndRotation(_camera.transform.position, Quaternion.Euler(targetXAngle, targetYAngle, DefaultZRotation));
    }

    private void Scope()
    {
        _isScopeMode = true;
        _pointerUpArea.enabled = true;

        if (IsMobile)
            _pointerDownArea.gameObject.SetActive(false);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetFieldOfView(Settings.Shooting.ScopeFieldOfView));
        Scoped?.Invoke();
    }

    private void Unscope()
    {
        _isScopeMode = false;
        _pointerUpArea.enabled = false;

        if (IsMobile)
            _pointerDownArea.gameObject.SetActive(true);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetFieldOfView(Settings.Shooting.BaseFieldOfView));
        Unscoped?.Invoke();
    }

    private IEnumerator SetFieldOfView(float value)
    {
        while (Mathf.Abs(_camera.fieldOfView - value) > Settings.Camera.FieldOfViewEpsilon)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, value, Settings.Shooting.ScopeSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    private void Shoot()
    {
        if (InputMode == InputMode.Game)
        {
            Vector3 camera—enter = new Vector3(0.5f, 0.5f, 0);
            Ray ray = _camera.ViewportPointToRay(camera—enter);
            Physics.Raycast(ray, out RaycastHit hit);
            Shooted?.Invoke(hit);
            Unscope();
        }
    }

    private void OnNewLevelLoaded()
    {
        _xUpperBound = (_level.CurrentLevel.CameraTransform.eulerAngles.x + Settings.Camera.XMaxRotation) % 360;
        _xLowerBound = _level.CurrentLevel.CameraTransform.eulerAngles.x - Settings.Camera.XMaxRotation;

        if (_xLowerBound < 0)
            _xLowerBound += 360;

        _yUpperBound = (_level.CurrentLevel.CameraTransform.eulerAngles.y + Settings.Camera.YMaxRotation) % 360;
        _yLowerBound = _level.CurrentLevel.CameraTransform.eulerAngles.y - Settings.Camera.YMaxRotation;

        if (_yLowerBound < 0)
            _yLowerBound += 360;

        Unscope();
    }

    private void OnLevelFinished()
    {

    }
}