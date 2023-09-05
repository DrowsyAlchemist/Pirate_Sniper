using UnityEngine;

public class WatchTask : ReadTask
{
    [SerializeField] private RectTransform _highlightedObject;

    protected override void Begin()
    {
        TrainingPanel.SetHighlightedObject(_highlightedObject);
        base.Begin();
    }

    protected override void OnComplete()
    {
        TrainingPanel.HideFadePanel();
        base.OnComplete();
    }
}
