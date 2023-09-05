using System;
using System.Collections;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    [SerializeField][TextArea(5, 15)] private string _note;
    [SerializeField] private float _delayBeforeTask;
    [SerializeField] private bool _isNotePanelInLeftCorner;

    private static Stopwatch _stopWatch;

    public event Action Completed;

    protected TrainingPanel TrainingPanel { get; private set; }

    private void Awake()
    {
        _stopWatch ??= new Stopwatch();
    }

    public void Begin(TrainingPanel trainingPanel)
    {
        Settings.CoroutineObject.StartCoroutine(BeginTaskWithDelay(trainingPanel));
    }

    protected abstract void BeginTask();

    protected virtual void OnComplete()
    {
        TrainingPanel.Deactivate();
    }

    protected void Complete()
    {
        OnComplete();
        Completed?.Invoke();
    }

    private IEnumerator BeginTaskWithDelay(TrainingPanel trainingPanel)
    {
        _stopWatch.Reset();
        _stopWatch.Start();

        while (_stopWatch.ElapsedTime < _delayBeforeTask)
            yield return null;

        TrainingPanel = trainingPanel;
        trainingPanel.SetNote(_note);
        TrainingPanel.SetContinueButtonActive(false);
        TrainingPanel.HideFadePanel();
        TrainingPanel.Activate();

        if (_isNotePanelInLeftCorner)
            trainingPanel.SetInLeftCorner();
        else
            trainingPanel.SetInRightCorner();

        BeginTask();
    }
}
