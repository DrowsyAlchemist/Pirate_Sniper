using System;
using UnityEngine;

public class TrainingOfferWindow : Window
{
    [SerializeField] private UIButton _agreeButton;
    [SerializeField] private UIButton _cancelButton;

    public event Action AgreeButtonClicked;
    public event Action CancelButtonClicked;

    private void Awake()
    {
        _agreeButton.AddOnClickAction(OnAgreeButtonClick);
        _cancelButton.AddOnClickAction(OnCancelButtonClick);
    }

    public override void Open()
    {
        base.Open();
        InputController.SetMode(InputMode.UI);
    }

    public override void Close()
    {
        base.Close();
        InputController.SetMode(InputMode.Game);
    }

    private void OnAgreeButtonClick()
    {
        Close();
        AgreeButtonClicked?.Invoke();
    }

    private void OnCancelButtonClick()
    {
        Close();
        CancelButtonClicked?.Invoke();
    }
}
