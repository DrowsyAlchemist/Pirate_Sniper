using UnityEngine;
using UnityEngine.UI;

public class ClickTask : StopTimeTask
{
    [SerializeField] private Button _targetButton;

    protected override void BeginTask()
    {
        _targetButton.AddListener(Complete);
        base.BeginTask();
    }

    protected override void OnComplete()
    {
        _targetButton.RemoveListener(Complete);
        base.OnComplete();
    }
}
