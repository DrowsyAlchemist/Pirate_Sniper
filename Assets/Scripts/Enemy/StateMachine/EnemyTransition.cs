using UnityEngine;

[RequireComponent(typeof(EnemyBody))]
public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;
    private EnemyBody _enemyBody;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    protected Enemy Enemy => _enemyBody.Enemy;
    protected Player Player => Enemy.Target;

    private void Awake()
    {
        enabled = false;
        _enemyBody = GetComponent<EnemyBody>();
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
