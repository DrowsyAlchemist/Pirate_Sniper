using UnityEngine;

public abstract class StopTimeTask : Task
{
    protected override void Begin()
    {
        Time.timeScale = 0;
    }

    protected override void OnComplete()
    {
        Time.timeScale = 1;
    }
}
