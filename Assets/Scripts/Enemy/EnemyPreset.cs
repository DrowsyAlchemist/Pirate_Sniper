using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 51)]
public class EnemyPreset : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _headshotDamageModifier = 1.5f;

    public int Health => _health;
    public int Damage => _damage;
    public float HeadshotDamageModifier => _headshotDamageModifier;
}
