using UnityEngine;

[RequireComponent(typeof(EnemyBody))]
public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;

    private EnemyBody _enemyBody;

    protected Enemy Enemy => _enemyBody.Enemy;
    protected Player Player => Enemy.Target;

    private void Awake()
    {
        enabled = false;
        _enemyBody = GetComponent<EnemyBody>();
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
