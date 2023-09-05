using UnityEngine;

public abstract class StopTimeTask : Task
{
    protected override void BeginTask()
    {
        Time.timeScale = 0;
    }

    protected override void OnComplete()
    {
        base.OnComplete();
        Time.timeScale = 1;
    }
}
