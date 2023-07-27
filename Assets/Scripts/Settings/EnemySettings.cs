using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Settings/Enemy")]
public class EnemySettings : ScriptableObject
{
    [SerializeField] private int _maxHealth = 100;

    public int MaxHealth => _maxHealth;
}
