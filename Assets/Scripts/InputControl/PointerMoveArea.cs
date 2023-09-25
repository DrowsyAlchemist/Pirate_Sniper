using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerMoveArea : MonoBehaviour, IPointerMoveHandler
{
    private InputHandler _inputHandler;
    private Vector2 _previousPosition;

    public event Action PointerMove;

    private void Awake()
    {
        enabled = false;
    }

    public void Init(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void OnEnable()
    {
        _previousPosition = Input.mousePosition;
    }

    private void Update()
    {
        if (_inputHandler.IsMobile == false)
            PointerMove?.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (_inputHandler.IsMobile)
        {
            if (Vector2.Distance(eventData.position, _previousPosition) > 60)
            {
                _previousPosition = eventData.position;
                return;
            }
            _previousPosition = eventData.position;
            PointerMove?.Invoke();
        }
    }
}
