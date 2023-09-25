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

    private InputHandler _inputHandler;
    private static Stopwatch _stopWatch;

    public InputMode InitialInputMode { get; private set; }
    protected TrainingPanel TrainingPanel { get; private set; }
    protected InputHandler InputHandler => _inputHandler;

    public event Action Completed;

    private void Awake()
    {
        _stopWatch ??= new Stopwatch();
    }

    public void Init(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    public void Begin(TrainingPanel trainingPanel)
    {
        Settings.CoroutineObject.StartCoroutine(BeginTaskWithDelay(trainingPanel));
    }

    public void ForceComplete()
    {
        Complete();
    }

    protected abstract void BeginTask();

    protected virtual void OnComplete()
    {
        TrainingPanel.Deactivate();
    }

    protected void Complete()
    {
        if (InputHandler.InputMode != InitialInputMode)
        {
            if (InputHandler.InputMode == InputMode.UI)
                InputHandler.SetGameMode();
            else
                InputHandler.SetUIMode();
        }

        OnComplete();
        Completed?.Invoke();
    }

    private IEnumerator BeginTaskWithDelay(TrainingPanel trainingPanel)
    {
        _stopWatch.ReStart();

        while (_stopWatch.ElapsedTime < _delayBeforeTask)
            yield return null;

        InitialInputMode = InputHandler.InputMode;
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
