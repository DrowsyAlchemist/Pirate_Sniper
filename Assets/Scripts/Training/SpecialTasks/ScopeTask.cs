using System.Collections;
using UnityEngine;

public class ScopeTask : Task
{
    [SerializeField] private InputHandler _inputController;
    [SerializeField] private RectTransform _scopeButton;
    [SerializeField] private float _completeDelay;

    protected override void BeginTask()
    {
        _inputController.Scoped += OnScoped;

        if (InputHandler.IsMobile)
            TrainingPanel.SetHighlightedObject(_scopeButton);
    }

    protected override void OnComplete()
    {
        _inputController.Scoped -= OnScoped;
        base.OnComplete();
    }

    private void OnScoped()
    {
        StartCoroutine(CompleteWithDelay());
    }

    private IEnumerator CompleteWithDelay()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();

        while (stopwatch.ElapsedTime < _completeDelay)
            yield return null;

        Complete();
    }
}
