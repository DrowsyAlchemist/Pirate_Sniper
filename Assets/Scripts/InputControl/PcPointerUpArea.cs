using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PcPointerUpArea : MonoBehaviour, IPointerUpHandler, IPointerUpArea
{
    public event Action PointerUp;

    public void CheckForMouseUp()
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp?.Invoke();
    }
}
