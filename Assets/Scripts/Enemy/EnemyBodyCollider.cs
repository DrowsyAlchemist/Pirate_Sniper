using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyBodyCollider : MonoBehaviour, IApplyDamage
{
    public event Action<int> Damaged;

    public void ApplyDamage(int damage)
    {
        Debug.Log(gameObject.name);
        Damaged?.Invoke(damage);
    }
}
