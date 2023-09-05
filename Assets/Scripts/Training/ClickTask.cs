using UnityEngine;

public class ClickTask : StopTimeTask
{
    [SerializeField] private RectTransform _highlightedObject;
    [SerializeField] private UIButton _targetButton;

    protected override void BeginTask()
    {
        TrainingPanel.SetHighlightedObject(_highlightedObject);
        _targetButton.AddOnClickAction(Complete);
        base.BeginTask();
    }

    protected override void OnComplete()
    {
        TrainingPanel.HideFadePanel();
        _targetButton.RemoveOnClickAction(Complete);
        base.OnComplete();
    }
}
