using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _coroutineObject;
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private ShootingSettings _shootingSettings;
    [SerializeField] private CameraSettings _cameraSettings;
    [SerializeField] private LeaderboardSettings _leaderboardSettings;

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
    public static PlayerSettings PlayerSettings => _instance._playerSettings;
    public static ShootingSettings ShootingSettings => _instance._shootingSettings;
    public static CameraSettings CameraSettings => _instance._cameraSettings;
    public static LeaderboardSettings LeaderboardSettings => _instance._leaderboardSettings;
}
