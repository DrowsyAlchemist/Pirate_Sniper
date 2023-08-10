using UnityEngine;

public class EnemyBody : MonoBehaviour, IApplyDamage
{
    [SerializeField] private EnemyPreset _preset;
    [SerializeField] private EnemyHead _head;
    [SerializeField] private DamageRenderer _damageRenderer;
    [SerializeField] private EnemyAnimator _animator;

    public Enemy Enemy { get; private set; }

    public void Init(Player player)
    {
        Enemy = new Enemy(_preset, player, _animator);
        _head.Damaged += OnHeadshot;
    }

    private void OnDestroy()
    {
        _head.Damaged -= OnHeadshot;
    }

    public void ApplyDamage(int damage)
    {
        Enemy.ApplyDamage(damage);
        _damageRenderer.Show(damage, isHeadshot: false);
    }

    private void OnHeadshot(int damage)
    {
        int increasedDamage = Enemy.ApplyHeadshot(damage);
        _damageRenderer.Show(increasedDamage, isHeadshot: true);
    }
}
