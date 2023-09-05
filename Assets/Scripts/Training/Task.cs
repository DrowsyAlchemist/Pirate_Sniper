using System;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    [SerializeField][TextArea(5, 15)] private string _note;
    [SerializeField] private bool _isNotePanelInLeftCorner;
    [SerializeField] private bool _isContinueButtonActive;

    public event Action Completed;

    protected TrainingPanel TrainingPanel { get; private set; }

    public void Init(TrainingPanel trainingPanel)
    {
        TrainingPanel = trainingPanel;
        trainingPanel.SetNote(_note);
        TrainingPanel.SetContinueButtonActive(false);
        TrainingPanel.HideFadePanel();

        if (_isNotePanelInLeftCorner)
            trainingPanel.SetInLeftCorner();
        else
            trainingPanel.SetInRightCorner();
    }

    protected abstract void Begin();

    protected virtual void OnComplete()
    {
    }

    protected void Complete()
    {
        OnComplete();
        Completed?.Invoke();
    }
}
