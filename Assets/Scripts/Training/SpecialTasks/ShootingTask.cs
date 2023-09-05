using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTask : Task
{
    [SerializeField] private InputController _inputController;

    protected override void BeginTask()
    {
        _inputController.Shooted += OnShooted;
    }

    protected override void OnComplete()
    {
        _inputController.Shooted -= OnShooted;
        base.OnComplete();
    }

    private void OnShooted(RaycastHit _)
    {
        Complete();
    }
}
