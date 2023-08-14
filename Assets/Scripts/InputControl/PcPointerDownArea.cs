using System;
using UnityEngine;

public class PcPointerDownArea : MonoBehaviour, IPointerDownArea
{
    public event Action PointerDown;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            PointerDown?.Invoke();
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
