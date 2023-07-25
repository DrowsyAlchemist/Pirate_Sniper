using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler
{
    [SerializeField] private float _xMinRotation;
    [SerializeField] private float _xMaxRotation;
    [SerializeField] private float _yMinRotation;
    [SerializeField] private float _yMaxRotation;

    [SerializeField] private Camera _camera;
    [SerializeField] private Quaternion _initialRotation;
    [SerializeField] private float _xSensitivity = 100;
    [SerializeField] private float _ySensitivity = 100;

    private void Awake()
    {
        Cursor.visible = false;
        _initialRotation = _camera.transform.rotation;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Vector3 cameraEulers = _camera.transform.localRotation.eulerAngles;
        Vector3 cameraAngles = cameraEulers;

        cameraAngles.x -= (cameraEulers.x > 180) ? 360 : 0;
        cameraAngles.y -= (cameraEulers.y > 180) ? 360 : 0;

        Debug.Log(cameraAngles);

        float targetXAngle = cameraAngles.x + Time.deltaTime * _ySensitivity * -1 * Input.GetAxis("Mouse Y");

        if (targetXAngle > _xMaxRotation)
            targetXAngle = _xMaxRotation;

        if (targetXAngle < _xMinRotation)
            targetXAngle = _xMinRotation;

        float targetYAngle = cameraAngles.y + Time.deltaTime * _xSensitivity * Input.GetAxis("Mouse X");

        if (targetYAngle > _yMaxRotation)
            targetYAngle = _yMaxRotation;

        if (targetYAngle < _yMinRotation)
            targetYAngle = _yMinRotation;

        targetXAngle += (cameraEulers.x < 0) ? 360 : 0;
        targetYAngle -= (cameraEulers.y < 0) ? 360 : 0;
        float targetZAngle = 0;

        _camera.transform.SetPositionAndRotation(_camera.transform.position, Quaternion.Euler(targetXAngle, targetYAngle, targetZAngle));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = _camera.ScreenPointToRay(rayStartPosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Debug.Log(hit.collider.gameObject.name);
    }
}
