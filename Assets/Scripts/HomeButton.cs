using System;
using UnityEngine;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private bool _isMobile;

    public Action Clicked;

    private void OnDestroy()
    {
        if (_isMobile)
            _button.RemoveListener(OnButtonClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            OnButtonClick();
    }

    public void Init(bool isMobile)
    {
        _isMobile = isMobile;
        enabled = (isMobile == false);

        if (_isMobile)
            _button.AddListener(OnButtonClick);
        else
            _button.Deactivate();
    }

    private void OnButtonClick()
    {
        Clicked?.Invoke();
    }
}
