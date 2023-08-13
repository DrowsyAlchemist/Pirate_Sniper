using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] private EnemyPreset _preset;
    [SerializeField] private EnemyHead _headCollider;
    [SerializeField] private EnemyBodyCollider[] _bodyColliders;
    [SerializeField] private DamageRenderer _damageRenderer;
    [SerializeField] private EnemyAnimator _animator;

    public Enemy Enemy { get; private set; }

    public void Init(Player player)
    {
        Enemy = new Enemy(_preset, player, _animator);
        _headCollider.Damaged += OnHeadshot;

        foreach (var collider in _bodyColliders)
            collider.Damaged += OnBodyShot;
    }

    private void OnDestroy()
    {
        _headCollider.Damaged -= OnHeadshot;

        foreach (var collider in _bodyColliders)
            collider.Damaged -= OnBodyShot;
    }

    public void OnBodyShot(int damage)
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
