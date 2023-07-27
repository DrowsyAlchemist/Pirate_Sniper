using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    private Button _button;
    private UnityAction _onClick;
    private bool _isInitialized;

    protected void Awake()
    {
        _button = GetComponent<Button>();
    }

    protected void OnDestroy()
    {
        if (_isInitialized)
            _button.RemoveListener(_onClick);
    }

    protected void SetOnClickAction(UnityAction onClick)
    {
        _onClick = onClick;
        _button.AddListener(_onClick);
        _isInitialized = true;
    }
}
