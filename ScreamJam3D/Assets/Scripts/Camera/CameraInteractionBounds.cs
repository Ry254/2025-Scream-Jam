using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PlayerLookState
{
    None           = 0b000000,
    LeftWindow     = 0b000001,
    SteeringWheel  = 0b000010,
    RightWindow    = 0b000100,
    Pedals         = 0b001000,
    Fridge         = 0b010000,
    SunRoof        = 0b100000,
    
    RightFrames    = 0b010100,
    Verticals      = 0b101010
}

[Serializable]
public struct FrameBounds
{
    [SerializeField]
    public PlayerLookState direction;
    [SerializeField]
    public List<GameObject> bounds;
}