using System;
using System.Collections;
using UnityEngine;
using Agava.WebUtility;
using cakeslice;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PointerMoveArea _pointerMoveArea;
    [SerializeField] private PointerDownArea _pointerDownArea;
    [SerializeField] private PointerUpArea _pointerUpArea;
    [SerializeField] private OutlineEffect _outlineEffect;

    [SerializeField] private PauseButton _pauseButton;

    [SerializeField] private Level _level;

    private const float DefaultZRotation = 0;
    private const float AngleEpsilon = 70;
    private const float CameraSpeedTrashhold = 5000;
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

    public bool IsMobile { get; private set; }
    public InputMode InputMode { get; private set; }
    public float CurrentSensitivity => _isScopeMode ? _sensitivity.ScopeSensitivity : _sensitivity.BaseSensitivity;

    private void OnDestroy()
    {
        _pointerMoveArea.PointerMove -= OnPointerMove;
        _pointerDownArea.PointerDown -= Scope;
        _pointerUpArea.PointerUp -= Shoot;
        _level.LevelLoaded -= OnNewLevelLoaded;
        WebApplication.InBackgroundChangeEvent -= OnBackgroundChanged;
    }

    public void Init(bool isMobile, Sensitivity sensitivity)
    {
        IsMobile = isMobile;
        _sensitivity = sensitivity;
        SetUIMode();
        _pauseButton.Init(isMobile);
        _pointerDownArea.Init(this);
        _pointerMoveArea.Init(this);
        _pointerDownArea.PointerDown += Scope;
        _pointerUpArea.PointerUp += Shoot;
        _pointerMoveArea.PointerMove += OnPointerMove;
        _level.LevelLoaded += OnNewLevelLoaded;
        WebApplication.InBackgroundChangeEvent += OnBackgroundChanged;
    }

    public void SetUIMode()
    {
        InputMode = InputMode.UI;
        _outlineEffect.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _pointerMoveArea.enabled = false;
        _pointerUpArea.enabled = false;
        _pointerDownArea.enabled = false;
    }

    public void SetGameMode()
    {
        InputMode = InputMode.Game;

        if (IsMobile)
            _outlineEffect.enabled = true;
        else
            Cursor.lockState = CursorLockMode.Locked;

        _pointerMoveArea.enabled = true;
        _pointerUpArea.enabled = true;
        _pointerDownArea.enabled = true;
        Unscope();
    }

    private void OnBackgroundChanged(bool isOut)
    {
        if (isOut)
            _level.PauseGame();
    }

    public void OnPointerMove()
    {
        Vector3 cameraAngles = _camera.transform.localRotation.eulerAngles;
        float targetXAngle = CalculateXAngle(cameraAngles.x);
        float targetYAngle = CalculateYAngle(cameraAngles.y);
        _camera.transform.SetPositionAndRotation(_camera.transform.position, Quaternion.Euler(targetXAngle, targetYAngle, DefaultZRotation));
    }

    private float CalculateXAngle(float currentXAngle)
    {
        float inputX = Input.GetAxis("Mouse Y");
        float cameraXSpeed = -1 * CurrentSensitivity * inputX;

        if (IsMobile && Mathf.Abs(cameraXSpeed) > CameraSpeedTrashhold)
            cameraXSpeed = 0;

        float targetXAngle = currentXAngle + cameraXSpeed * Time.deltaTime;
        float xAngleToCheck = targetXAngle % 360;

        if ((xAngleToCheck - _xLowerBound) < 0 && (xAngleToCheck - _xLowerBound) > -1 * AngleEpsilon
            || (xAngleToCheck - _xUpperBound) > 0 && (xAngleToCheck - _xUpperBound) < AngleEpsilon)
            return currentXAngle;

        return targetXAngle;
    }

    private float CalculateYAngle(float currentYAngle)
    {
        float inputY = Input.GetAxis("Mouse X");
        float cameraYSpeed = CurrentSensitivity * inputY;

        if (IsMobile && Mathf.Abs(cameraYSpeed) > CameraSpeedTrashhold)
            cameraYSpeed = 0;

        float targetYAngle = currentYAngle + cameraYSpeed * Time.deltaTime;
        float yAngleToCheck = targetYAngle % 360;

        if ((yAngleToCheck - _yLowerBound) < 0 && (yAngleToCheck - _yLowerBound) > -1 * AngleEpsilon
            || (yAngleToCheck - _yUpperBound) > 0 && (yAngleToCheck - _yUpperBound) < AngleEpsilon)
            return currentYAngle;

        return targetYAngle;
    }

    private void Scope()
    {
        if (InputMode == InputMode.UI)
            return;

        _isScopeMode = true;
        _pointerDownArea.enabled = false;
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
        _pointerDownArea.enabled = true;

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
}