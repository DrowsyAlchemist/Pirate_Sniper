using System.Collections;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IApplyDamage
{
    [SerializeField] private EnemyPreset _preset;
    [SerializeField] private CharacterAnimator _animator;
    [SerializeField] private EnemyHead _head;
    [SerializeField] private DamageRenderer _damageRenderer;

    public Enemy Enemy { get; private set; }

    public void Init(Player player)
    {
        Enemy = new Enemy(_preset, player);
        Enemy.ReadonlyHealth.Dead += OnDead;
        _head.Damaged += OnHeadshot;
    }

    private void OnDestroy()
    {
        Enemy.ReadonlyHealth.Dead -= OnDead;
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

    private void OnDead()
    {
        Settings.CoroutineObject.StartCoroutine(PlayDead());
    }

    private IEnumerator PlayDead()
    {
        yield return new WaitForSeconds(1.5f);
        //Destroy(gameObject);
    }
}
