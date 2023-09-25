using System;
using UnityEngine;

public class TrainingOfferWindow : Window
{
    [SerializeField] private UIButton _agreeButton;
    [SerializeField] private UIButton _cancelButton;

    private InputHandler _inputHandler;

    public event Action AgreeButtonClicked;
    public event Action CancelButtonClicked;

    private void Awake()
    {
        _agreeButton.AddOnClickAction(OnAgreeButtonClick);
        _cancelButton.AddOnClickAction(OnCancelButtonClick);
    }

    public void Init(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    public override void Open()
    {
        base.Open();
        _inputHandler.SetUIMode();
    }

    public override void Close()
    {
        base.Close();
        _inputHandler.SetGameMode();
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
