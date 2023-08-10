using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : EnemyState
{
    private void OnEnable()
    {
        Enemy.Animator.PlayDeath();
    }
}
