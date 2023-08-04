using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 51)]
public class EnemyPreset : ScriptableObject
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _minSecondsBetweenShots;
    [SerializeField] private float _maxSecondsBetweenShots;
    [SerializeField, Range(0, 100)] private float _accuracyInPercents;

    [SerializeField] private float _headshotDamageModifier = 1.5f;

    public int Health => _health;
    public int Damage => _damage;
    public float MinSecondsBetweenShots => _minSecondsBetweenShots;
    public float MaxSecondsBetweenShots => _maxSecondsBetweenShots;
    public float AccuracyInPercents => _accuracyInPercents;
    public float HeadshotDamageModifier => _headshotDamageModifier;
}
