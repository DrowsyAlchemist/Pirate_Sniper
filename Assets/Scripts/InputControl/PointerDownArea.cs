using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerDownArea : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _image;

    public event Action PointerDown;

    public void Init()
    {
        _image.gameObject.SetActive(InputController.IsMobile);
    }

    private void Update()
    {
        if (InputController.IsMobile == false)
            if (Input.GetMouseButtonDown(0))
                PointerDown?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (InputController.IsMobile)
            PointerDown?.Invoke();
    }
}
