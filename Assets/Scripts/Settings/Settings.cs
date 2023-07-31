using System;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _coroutineObject;
    [SerializeField] private ShootingSettings _shooting;
    [SerializeField] private CameraSettings _camera;
    [SerializeField] private ScoreSettings _score;
    [SerializeField] private MoneySettings _money;
    [SerializeField] private LeaderboardSettings _leaderboard;

    [SerializeField] private PlayerCharacteristics _characteristics;

    public const float Epsilon = 0.5f;
    private static Settings _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static MonoBehaviour CoroutineObject => _instance._coroutineObject;
    public static ShootingSettings Shooting => _instance._shooting;
    public static CameraSettings Camera => _instance._camera;
    public static ScoreSettings Score => _instance._score;
    public static MoneySettings Money => _instance._money;
    public static LeaderboardSettings Leaderboard => _instance._leaderboard;
    public static PlayerCharacteristics Characteristics => _instance._characteristics;

    [Serializable]
    public class PlayerCharacteristics
    {
        [SerializeField] private Characteristic _health;
        [SerializeField] private Characteristic _damage;

        public Characteristic Health => _health;
        public Characteristic Damage => _damage;
    }
}
