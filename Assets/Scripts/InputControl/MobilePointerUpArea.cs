using System;
using UnityEngine;

public class MobilePointerUpArea : MonoBehaviour, IPointerUpArea
{
    private bool _isChecking;

    public event Action PointerUp;

    private void Update()
    {
        if (_isChecking && Input.GetMouseButtonUp(0))
        {
            PointerUp?.Invoke();
            _isChecking = false;
        }
    }

    public void CheckForMouseUp()
    {
        _isChecking = true;
    }
}
