using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PcPointerUpArea : MonoBehaviour, IPointerUpHandler, IPointerUpArea
{
    public event Action PointerUp;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            PointerUp?.Invoke();
    }

    public void CheckForMouseUp()
    {
        enabled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp?.Invoke();
    }
}
