using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    protected Enemy Enemy { get; private set; }
    protected Player Target { get; private set; }


    private void Awake()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        Enemy = GetComponent<Enemy>();
        Target = GetComponent<Enemy>().Target;
        NeedTransit = false;
    }
}
