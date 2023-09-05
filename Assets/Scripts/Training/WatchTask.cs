using UnityEngine;

public class WatchTask : ReadTask
{
    [SerializeField] private RectTransform _highlightedObject;

    protected override void BeginTask()
    {
        InputController.SetMode(InputMode.UI);
        TrainingPanel.SetHighlightedObject(_highlightedObject);
        base.BeginTask();
    }

    protected override void OnComplete()
    {
        InputController.SetMode(InputMode.Game);
        TrainingPanel.HideFadePanel();
        base.OnComplete();
    }
}
