using UnityEngine;

[RequireComponent(typeof(Enemy), typeof(EnemyAnimator))]
public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;

    protected Enemy Enemy { get; private set; }
    protected Player Target => Enemy.Target;
    protected EnemyAnimator EnemyAnimator { get; private set; }

    private void Awake()
    {
        enabled = false;
        EnemyAnimator = GetComponent<EnemyAnimator>();
        Enemy = GetComponent<Enemy>();
      //  Target = Enemy.Target;
    }

    public void Enter()
    {
        enabled = true;

        foreach (var transition in _transitions)
            transition.enabled = true;
    }

    public void Exit()
    {
        foreach (var transition in _transitions)
            transition.enabled = false;

        enabled = false;
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
            if (transition.NeedTransit)
                return transition.TargetState;

        return null;
    }
}
