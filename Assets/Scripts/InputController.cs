using UnityEngine;

public class InputController
{
    private Camera _camera;
    private Quaternion _initialRotation;
    private PointerMoveArea _pointerMoveArea;
    private ShootingButton _shootingButton;

    private float _currentSensitivity;

    public InputController(Camera camera, PointerMoveArea pointerMoveArea, ShootingButton shootingButton)
    {
        _camera = camera;
        _initialRotation = _camera.transform.rotation;

        _pointerMoveArea = pointerMoveArea;
        _pointerMoveArea.PointerMove += OnPointerMove;

        _shootingButton = shootingButton;
        _shootingButton.PointerDown += Scope;
        _shootingButton.PointerUp += Shoot;

        _currentSensitivity = Settings.ShootingSettings.BaseSensitivity;
    }

    ~InputController()
    {
        _pointerMoveArea.PointerMove -= OnPointerMove;
        _shootingButton.PointerDown -= Scope;
        _shootingButton.PointerUp -= Shoot;
    }

    public void OnPointerMove()
    {
        Vector3 cameraEulers = _camera.transform.localRotation.eulerAngles;
        Vector3 cameraAngles = cameraEulers;

        cameraAngles.x -= (cameraEulers.x > 180) ? 360 : 0;
        cameraAngles.y -= (cameraEulers.y > 180) ? 360 : 0;

        float targetXAngle = cameraAngles.x + Time.deltaTime * _currentSensitivity * -1 * Input.GetAxis("Mouse Y");

        if (targetXAngle > Settings.CameraSettings.XMaxRotation)
            targetXAngle = Settings.CameraSettings.XMaxRotation;

        if (targetXAngle < Settings.CameraSettings.XMinRotation)
            targetXAngle = Settings.CameraSettings.XMinRotation;

        float targetYAngle = cameraAngles.y + Time.deltaTime * _currentSensitivity * Input.GetAxis("Mouse X");

        if (targetYAngle > Settings.CameraSettings.YMaxRotation)
            targetYAngle = Settings.CameraSettings.YMaxRotation;

        if (targetYAngle < Settings.CameraSettings.YMinRotation)
            targetYAngle = Settings.CameraSettings.YMinRotation;

        targetXAngle += (cameraEulers.x < 0) ? 360 : 0;
        targetYAngle -= (cameraEulers.y < 0) ? 360 : 0;
        float targetZAngle = _initialRotation.z;

        _camera.transform.SetPositionAndRotation(_camera.transform.position, Quaternion.Euler(targetXAngle, targetYAngle, targetZAngle));
    }

    private void Scope()
    {
        _camera.fieldOfView = Settings.ShootingSettings.ScopeFieldOfView;
        _currentSensitivity = Settings.ShootingSettings.ScopeSensitivity;
    }

    private void Unscope()
    {
        _camera.fieldOfView = Settings.ShootingSettings.BaseFieldOfView;
        _currentSensitivity = Settings.ShootingSettings.BaseSensitivity;
    }

    private void Shoot()
    {
        Vector3 cameraÑenter = new Vector3(0.5f, 0.5f, 0);
        Ray ray = _camera.ViewportPointToRay(cameraÑenter);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Unscope();

        Debug.Log("Shoot! Hit: " + hit.collider.gameObject.name);
    }
}
