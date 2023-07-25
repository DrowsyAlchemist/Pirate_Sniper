using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDownArea : MonoBehaviour, IPointerDownHandler, IPointerDownArea
{
    public event Action PointerDown;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown?.Invoke();
    }
}
