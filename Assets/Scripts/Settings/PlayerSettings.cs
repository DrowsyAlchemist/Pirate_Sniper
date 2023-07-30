using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Settings/Player")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private int _initialHealth = 100;
    [SerializeField] private int _initialDamage = 50;

    public int InitialHealth => _initialHealth;
    public int InitialDamage => _initialDamage;
}
