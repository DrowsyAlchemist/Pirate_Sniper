using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 51)]
public class EnemyPreset : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _secondsBetweenShots;
    [SerializeField, Range(0, 100)] private float _accuracyInPercents;

    [SerializeField] private float _headshotDamageModifier = 1.5f;

    public int Health => _health;
    public int Damage => _damage;
    public float SecondsBetweenShots => _secondsBetweenShots;
    public float AccuracyInPercents => _accuracyInPercents;
    public float HeadshotDamageModifier => _headshotDamageModifier;
}
