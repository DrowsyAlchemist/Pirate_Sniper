using TMPro;
using UnityEngine;

public class LevelInfoRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemiesText;

    private LevelObserver _levelObserver;

    public void Init(LevelObserver levelObserver)
    {
        _levelObserver = levelObserver;
        _levelObserver.EnemyDead += OnEnemyDead;
    }

    private void OnDestroy()
    {
        _levelObserver.EnemyDead -= OnEnemyDead;
    }

    public void ResetInfo()
    {
        OnEnemyDead();
    }

    private void OnEnemyDead()
    {
        _enemiesText.text = _levelObserver.EnemiesLeft + " / " + _levelObserver.EnemiesCount;
    }
}
