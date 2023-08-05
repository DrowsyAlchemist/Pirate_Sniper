using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 51)]
public class WeaponInfo : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private int _damage;
    [SerializeField] private float _secondsBetweenShots;

    public string Id => _id;
    public string Lable => _lable;
    public Sprite Sprite => _sprite;
    public int Damage => _damage;
    public float SecondsBetweenShots => _secondsBetweenShots;
}
