using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerMoveArea : MonoBehaviour, IPointerMoveHandler
{
    public event Action PointerMove;

    private void Update()
    {
        if (InputController.IsMobile == false)
            PointerMove?.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (InputController.IsMobile)
            PointerMove?.Invoke();
    }
}
