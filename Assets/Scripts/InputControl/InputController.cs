using System;
using System.Collections;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private PointerMoveArea _pointerMoveArea;

    [SerializeField] private PointerDownArea _pcPointerDownArea;
    [SerializeField] private PointerDownArea _mobilePointerDownArea;

    [SerializeField] private PcPointerUpArea _pcPointerUpArea;
    [SerializeField] private MobilePointerUpArea _mobilePointerUpArea;

    [SerializeField] private HomeButton _homeButton;

    private IPointerDownArea _pointerDownArea;
    private IPointerUpArea _pointerUpArea;

    private Quaternion _initialRotation;
    private bool _isMobile;
    private bool _isScopeMode;
    private Coroutine _coroutine;

    public event Action<RaycastHit> Shooted;

    private float CurrentSensitivity => _isScopeMode ? Settings.Shooting.ScopeSensitivity : Settings.Shooting.BaseSensitivity;

    public void Init(bool isMobile)
    {
        _isMobile = isMobile;
        _mobilePointerDownArea.SetActive(isMobile);
        _initialRotation = _camera.transform.rotation;

        _pointerDownArea = isMobile ? _mobilePointerDownArea : _pcPointerDownArea;
        _pointerUpArea = isMobile ? _mobilePointerUpArea : _pcPointerUpArea;
        _homeButton.Init(isMobile);

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

        cameraAngles.x -= (cameraEulers.x > 180) ? 360 : 0;
        cameraAngles.y -= (cameraEulers.y > 180) ? 360 : 0;

        float targetXAngle = cameraAngles.x + Time.deltaTime * CurrentSensitivity * -1 * Input.GetAxis("Mouse Y");

        if (targetXAngle > Settings.Camera.XMaxRotation)
            targetXAngle = Settings.Camera.XMaxRotation;

        if (targetXAngle < Settings.Camera.XMinRotation)
            targetXAngle = Settings.Camera.XMinRotation;

        float targetYAngle = cameraAngles.y + Time.deltaTime * CurrentSensitivity * Input.GetAxis("Mouse X");

        if (targetYAngle > Settings.Camera.YMaxRotation)
            targetYAngle = Settings.Camera.YMaxRotation;

        if (targetYAngle < Settings.Camera.YMinRotation)
            targetYAngle = Settings.Camera.YMinRotation;

        targetXAngle += (cameraEulers.x < 0) ? 360 : 0;
        targetYAngle -= (cameraEulers.y < 0) ? 360 : 0;
        float targetZAngle = _initialRotation.z;

        _camera.transform.SetPositionAndRotation(_camera.transform.position, Quaternion.Euler(targetXAngle, targetYAngle, targetZAngle));
    }

    private void Scope()
    {
        _isScopeMode = true;

        if (_isMobile)
        {
            _pointerUpArea.CheckForMouseUp();
            _pointerDownArea.SetActive(false);
        }
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetFieldOfView(Settings.Shooting.ScopeFieldOfView));
    }

    private void Unscope()
    {
        _isScopeMode = false;

        if (_isMobile)
            _pointerDownArea.SetActive(true);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetFieldOfView(Settings.Shooting.BaseFieldOfView));
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
