using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    private Button _button;

    protected void OnDestroy()
    {
        if (_button != null)
        {
            _button.onClick.RemoveAllListeners();
            OnButtonDestroy();
        }
    }

    public void AddOnClickAction(UnityAction onClick)
    {
        if (_button == null)
            _button = GetComponent<Button>();

        _button.AddListener(onClick);
    }

    public void RemoveOnClickAction(UnityAction onClick)
    {
        _button.RemoveListener(onClick);
    }

    public void SetInteractable(bool value)
    {
        if (_button == null)
            _button = GetComponent<Button>();

        _button.interactable = value;
    }

    protected virtual void OnButtonDestroy()
    {
    }
}
