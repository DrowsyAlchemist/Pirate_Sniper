using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private EnemyBody[] _enemies;

    public IEnumerable<EnemyBody> Enemies => _enemies;

    public void Init()
    {
        foreach (EnemyBody enemy in _enemies)
            enemy.Init();
    }
}
