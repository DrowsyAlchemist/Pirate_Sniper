using UnityEngine;

[CreateAssetMenu(fileName = "MoneySettings", menuName = "Settings/Money")]
public class MoneySettings : ScriptableObject
{
    [SerializeField] private int _moneyForEachEnemy = 50;
    [SerializeField] private int _moneyForHeadshot = 20;

    public int MoneyForEachEnemy => _moneyForEachEnemy;
    public int MoneyForHeadshot => _moneyForHeadshot;
}
