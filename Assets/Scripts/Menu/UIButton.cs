using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    private Button _button;
    private UnityAction _onClick;

    protected void OnDestroy()
    {
        if (_button != null)
            _button.RemoveListener(_onClick);
    }

    public void SetOnClickAction(UnityAction onClick)
    {
        if (_button == null)
            _button = GetComponent<Button>();

        _onClick = onClick;
        _button.AddListener(_onClick);
    }
}
