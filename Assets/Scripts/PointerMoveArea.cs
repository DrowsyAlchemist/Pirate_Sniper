using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerMoveArea : MonoBehaviour, IPointerMoveHandler
{
    public event Action PointerMove;

    public void OnPointerMove(PointerEventData eventData)
    {
        PointerMove?.Invoke();
    }
}
