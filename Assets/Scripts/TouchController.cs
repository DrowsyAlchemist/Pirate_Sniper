using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController : MonoBehaviour, IPointerMoveHandler
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Button _shootButton;

    private void Awake()
    {
        _shootButton.AddListener(OnButtonClick);
    }

    private void OnDestroy()
    {
        _shootButton.RemoveListener(OnButtonClick);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void OnButtonClick()
    {
        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Ray ray = _camera.ScreenPointToRay(rayStartPosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Debug.Log(hit.collider.gameObject.name);
    }
}
