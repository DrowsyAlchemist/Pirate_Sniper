using UnityEngine;
using UnityEngine.UI;

public class ClickTask : StopTimeTask
{
    [SerializeField] private RectTransform _highlightedObject;
    [SerializeField] private Button _targetButton;

    protected override void BeginTask()
    {
        TrainingPanel.SetHighlightedObject(_highlightedObject);
        _targetButton.AddListener(Complete);
        base.BeginTask();
    }

    protected override void OnComplete()
    {
        TrainingPanel.HideFadePanel();
        _targetButton.RemoveListener(Complete);
        base.OnComplete();
    }
}
