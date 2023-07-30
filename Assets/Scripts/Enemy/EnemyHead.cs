using System;
using UnityEngine;

public class EnemyHead : MonoBehaviour, IApplyDamage
{
    public event Action<int> Damaged;

    public void ApplyDamage(int damage)
    {
        Damaged?.Invoke(damage);
    }
}
