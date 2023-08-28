using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSettings", menuName = "Settings/Score")]
public class ScoreSettings : ScriptableObject
{
    [SerializeField] private float _minTimeForEachEnemy = 3;
    [SerializeField] private float _completeTimeModifier = 400;

    [SerializeField] private float _accuracyModifier = 350;
    [SerializeField] private float _headshotsModifier = 250;

    [SerializeField] private int _oneStarScore = 400;
    [SerializeField] private int _twoStarsScore = 600;
    [SerializeField] private int _threeStarsScore = 750;

    public float MinTimeForEachEnemy => _minTimeForEachEnemy;
    public float CompleteTimeModifier => _completeTimeModifier;
    public float AccuracyModifier => _accuracyModifier;
    public float HeadshotsModifier => _headshotsModifier;
    public int OneStarScore => _oneStarScore;
    public int TwoStarsScore => _twoStarsScore;
    public float ThreeStarsScore => _threeStarsScore;

    public int GetStars(int score)
    {
        if (score >= _threeStarsScore)
            return 3;

        if (score >= _twoStarsScore)
            return 2;

        if (score >= _oneStarScore)
            return 1;

        return 0;
    }
}
