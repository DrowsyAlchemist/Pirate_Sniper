public class LevelInfo
{
    private readonly Level _level;

    public int _score;
    public int _stars;
    public float _time;

    public Level Level => _level;
    public int Score => _score;
    public int Stars => _stars;
    public float Time => _time;

    public LevelInfo(Level level)
    {
        _level = level;
    }
}