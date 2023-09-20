using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerMoveArea : MonoBehaviour, IPointerMoveHandler
{
    private InputHandler _inputHandler;

    public event Action PointerMove;

    private void Awake()
    {
        enabled = false;
    }

    public void Init(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Update()
    {
        if (_inputHandler.IsMobile == false)
            PointerMove?.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_inputHandler.IsMobile)
            PointerMove?.Invoke();
    }
}
