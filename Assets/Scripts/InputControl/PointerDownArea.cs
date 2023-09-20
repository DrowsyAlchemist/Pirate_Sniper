using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerDownArea : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _image;

    private InputHandler _inputHandler;

    public event Action PointerDown;

    private void Awake()
    {
        enabled = false;
    }

    public void Init(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _image.gameObject.SetActive(inputHandler.IsMobile);
    }

    private void Update()
    {
        if (_inputHandler.IsMobile == false)
            if (Input.GetMouseButtonDown(0))
                PointerDown?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_inputHandler.IsMobile)
            PointerDown?.Invoke();
    }
}
