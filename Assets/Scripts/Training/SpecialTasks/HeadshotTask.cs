using UnityEngine;

public class HeadshotTask : Task
{
    [SerializeField] private Level _level;

    protected override void BeginTask()
    {
        _level.LevelObserver.EnemyHeadshot += OnHeadshot;
    }

    protected override void OnComplete()
    {
        _level.LevelObserver.EnemyHeadshot -= OnHeadshot;
        base.OnComplete();
    }

    private void OnHeadshot()
    {
        Complete();
    }
}
