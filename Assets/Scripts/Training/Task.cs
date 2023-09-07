using Lean.Localization;
using System;
using System.Collections;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    [SerializeField] private RectTransform _parent;
    [SerializeField] private float _delayBeforeTask;
    [SerializeField] private bool _isNotePanelInRightCorner;
    [SerializeField] private string _localizationPhrase;
    [SerializeField][TextArea(5, 15)] private string _note;

    private static Stopwatch _stopWatch;
    private InputMode _initialInputMode;

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
        if (InputController.InputMode != _initialInputMode)
            InputController.SetMode(_initialInputMode);

        OnComplete();
        Completed?.Invoke();
    }

    private IEnumerator BeginTaskWithDelay(TrainingPanel trainingPanel)
    {
        _stopWatch.Reset();
        _stopWatch.Start();

        while (_stopWatch.ElapsedTime < _delayBeforeTask)
            yield return null;

        _initialInputMode = InputController.InputMode;
        TrainingPanel = trainingPanel;

        if (_isNotePanelInRightCorner)
            trainingPanel.SetInRightCorner();
        else
            trainingPanel.SetInLeftCorner();

        string localizedNote = LeanLocalization.GetTranslationText(_localizationPhrase);
        trainingPanel.SetNote(localizedNote);
        trainingPanel.transform.SetParent(_parent);
        trainingPanel.transform.SetSiblingIndex(_parent.childCount - 1);
        trainingPanel.Activate();
        BeginTask();
    }
}
