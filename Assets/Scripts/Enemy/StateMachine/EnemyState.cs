using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;

    protected Player Player { get; private set; }
    protected Enemy Enemy { get; private set; }

    private void Awake()
    {
        enabled = false;
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
