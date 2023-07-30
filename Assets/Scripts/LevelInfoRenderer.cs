using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelInfoRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemiesText;

    private LevelInfo _levelInfo;

    public void Init(LevelInfo levelInfo)
    {
        _levelInfo = levelInfo;
        _levelInfo.EnemyDead += OnEnemyDead;
    }

    private void OnDestroy()
    {
        _levelInfo.EnemyDead -= OnEnemyDead;
    }

    public void ResetInfo()
    {
        OnEnemyDead();
    }

    private void OnEnemyDead()
    {
        _enemiesText.text = _levelInfo.EnemiesLeft + " / " + _levelInfo.EnemiesCount;
    }
}
