using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerMoveArea : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
{
    private const float MaxInputDelta = 60;
    private InputHandler _inputHandler;
    private bool _isMoveAllowed;

    public event Action<Vector2> PointerMove;

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
        {
            Vector2 inputDelta = new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            PointerMove?.Invoke(inputDelta * Settings.Shooting.PCSensitivityModifier);
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_inputHandler.IsMobile)
        {
            if (_isMoveAllowed)
            {
                if (eventData.delta.magnitude > MaxInputDelta)
                    return;

                PointerMove?.Invoke(eventData.delta);
            }
            else
            {
                _isMoveAllowed = true;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isMoveAllowed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isMoveAllowed = false;
    }
}
