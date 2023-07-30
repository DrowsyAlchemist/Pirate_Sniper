using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _coroutineObject;
    [SerializeField] private PlayerSettings _player;
    [SerializeField] private ShootingSettings _shooting;
    [SerializeField] private CameraSettings _camera;
    [SerializeField] private ScoreSettings _score;
    [SerializeField] private LeaderboardSettings _leaderboard;

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
    public static PlayerSettings Player => _instance._player;
    public static ShootingSettings Shooting => _instance._shooting;
    public static CameraSettings Camera => _instance._camera;
    public static ScoreSettings Score => _instance._score;
    public static LeaderboardSettings Leaderboard => _instance._leaderboard;
}
