using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private LeaderboardSettings _leaderboardSettings;
    [SerializeField] private MonoBehaviour _coroutineObject;

    private static Settings _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static LeaderboardSettings LeaderboardSettings => _instance._leaderboardSettings;
    public static MonoBehaviour CoroutineObject => _instance._coroutineObject;
}
