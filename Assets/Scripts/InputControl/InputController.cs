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

    private static InputController _instance;
    private Quaternion _initialRotation;
    private bool _isScopeMode;
    private Coroutine _coroutine;

    public event Action Scoped;
    public event Action Unscoped;
    public event Action<RaycastHit> Shooted;

    public static bool IsMobile { get; private set; }
    public float CurrentSensitivity => _isScopeMode ? Settings.Shooting.ScopeSensitivity : Settings.Shooting.BaseSensitivity;
    public float ZeroXRotation => _level.CurrentLevel.CameraTransform.rotation.x;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static void SetMode(InputMode mode)
    {
        if (IsMobile)
            return;

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
            Cursor.lockState = CursorLockMode.Locked;
            _instance._pointerMoveArea.enabled = true;
            _instance._pointerUpArea.enabled = true;
            _instance._pointerDownArea.enabled = true;
            _instance.Unscope();
        }
    }

    public void Init(bool isMobile)
    {
        IsMobile = isMobile;
        SetMode(InputMode.UI);
        _initialRotation = _camera.transform.rotation;
        _pauseButton.Init(isMobile);
        _pointerDownArea.Init();
        _pointerDownArea.PointerDown += Scope;
        _pointerUpArea.PointerUp += Shoot;
        _pointerMoveArea.PointerMove += OnPointerMove;
    }

    private void OnDestroy()
    {
        _pointerMoveArea.PointerMove -= OnPointerMove;
        _pointerDownArea.PointerDown -= Scope;
        _pointerUpArea.PointerUp -= Shoot;
    }

    public void OnPointerMove()
    {
        Vector3 cameraEulers = _camera.transform.localRotation.eulerAngles;
        Vector3 cameraAngles = cameraEulers;

        cameraAngles.x %= 180;
        cameraAngles.y %= 180;

        //  float targetXAngle = cameraAngles.x + Time.deltaTime * CurrentSensitivity * -1 * Input.GetAxis("Mouse Y");

        // if (targetXAngle > Settings.Camera.XMaxRotation)
        //     targetXAngle = Settings.Camera.XMaxRotation;

        // if (targetXAngle < Settings.Camera.XMinRotation)
        //     targetXAngle = Settings.Camera.XMinRotation;

        //  float targetYAngle = cameraAngles.y + Time.deltaTime * CurrentSensitivity * Input.GetAxis("Mouse X");

        //  if (targetYAngle > Settings.Camera.YMaxRotation)
        //      targetYAngle = Settings.Camera.YMaxRotation;

        //  if (targetYAngle < Settings.Camera.YMinRotation)
        //      targetYAngle = Settings.Camera.YMinRotation;

        //  targetXAngle += (cameraEulers.x < 0) ? 360 : 0;
        // targetYAngle -= (cameraEulers.y < 0) ? 360 : 0;
        float targetXAngle = cameraEulers.x + Time.deltaTime * CurrentSensitivity * -1 * Input.GetAxis("Mouse Y");
        float targetYAngle = cameraEulers.y + Time.deltaTime * CurrentSensitivity * Input.GetAxis("Mouse X");

      //  Debug.Log(targetXAngle);

        float targetZAngle = _initialRotation.z;

        _camera.transform.SetPositionAndRotation(_camera.transform.position, Quaternion.Euler(targetXAngle, targetYAngle, targetZAngle));
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
        Vector3 camera—enter = new Vector3(0.5f, 0.5f, 0);
        Ray ray = _camera.ViewportPointToRay(camera—enter);
        Physics.Raycast(ray, out RaycastHit hit);
        Shooted?.Invoke(hit);
        Unscope();
    }
}