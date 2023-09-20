using System;
using UnityEngine;

[Serializable]
public class CharacteristicLevel
{
    [SerializeField] private int _value;
    [SerializeField] private int _cost;

    public int Value => _value;
    public int Cost => _cost;
}