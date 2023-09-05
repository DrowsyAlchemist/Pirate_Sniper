using UnityEngine;

public class ScopeTask : Task
{
    [SerializeField] private InputController _inputController;
    [SerializeField] private RectTransform _scopeButton;

    protected override void BeginTask()
    {
        _inputController.Scoped += OnScoped;

        if (InputController.IsMobile)
            TrainingPanel.SetHighlightedObject(_scopeButton);
    }

    protected override void OnComplete()
    {
        _inputController.Scoped -= OnScoped;
        base.OnComplete();
    }

    private void OnScoped()
    {
        Complete();
    }
}
