using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Location", order = 51)]
public class Location : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _requiredStars;
    [SerializeField] private List<Level> _levels;

    public string Lable => _lable;
    public Sprite Sprite => _sprite;
    public IReadOnlyList<Level> Levels => _levels;
    public int RequiredStars => _requiredStars;

    public bool IsCompleted 
    {
        get
        {
            foreach (var level in _levels)
                if (level.IsCompleted==false)
                    return false;

            return true;
        }
    }

    public int Stars
    {
        get
        {
            int stars = 0;

            foreach (var level in _levels)
                stars += level.Stars;

            return stars;
        }
    }
}
