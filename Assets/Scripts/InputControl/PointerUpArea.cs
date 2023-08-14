using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerUpArea : MonoBehaviour
{
    public event Action PointerUp;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            PointerUp?.Invoke();
    }
}
