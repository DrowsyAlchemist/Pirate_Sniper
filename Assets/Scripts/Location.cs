using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "Location", order = 51)]
public class Location : ScriptableObject
{
    [SerializeField] private string _lable;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private List<Level> _levels;

    public string Lable => _lable;
    public Sprite Sprite => _sprite;
    public IReadOnlyList<Level> Levels => _levels;
}
