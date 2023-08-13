using System.Collections.Generic;
using UnityEngine;

public class WeaponsStorage : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons;

    public IReadOnlyList<Weapon> Weapons => _weapons;
}
