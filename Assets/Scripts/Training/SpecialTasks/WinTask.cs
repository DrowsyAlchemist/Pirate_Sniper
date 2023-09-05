using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTask : Task
{
    [SerializeField] private Level _level;

    protected override void BeginTask()
    {
        _level.LevelObserver.Completed += OnCompleted;
    }

    protected override void OnComplete()
    {
        _level.LevelObserver.Completed -= OnCompleted;
        InputController.SetMode(InputMode.UI);
        base.OnComplete();
    }

    private void OnCompleted(bool isWon)
    {
        if (isWon)
            Complete();
    }
}
