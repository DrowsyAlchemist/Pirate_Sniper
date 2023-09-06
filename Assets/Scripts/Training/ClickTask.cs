using UnityEngine;
using UnityEngine.UI;

public class ClickTask : Task
{
    [SerializeField] private Button _targetButton;

    protected override void BeginTask()
    {
        _targetButton.AddListener(Complete);
    }

    protected override void OnComplete()
    {
        _targetButton.RemoveListener(Complete);
        base.OnComplete();
    }
}
