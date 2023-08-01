using TMPro;
using UnityEngine;

public class LevelInfoRenderer : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemiesText;
    [SerializeField] private HealthRenderer _playerHealthRenderer;

    private LevelObserver _levelObserver;

    public void Init(Player player, LevelObserver levelObserver)
    {
        _playerHealthRenderer.Init(player.Health);
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
