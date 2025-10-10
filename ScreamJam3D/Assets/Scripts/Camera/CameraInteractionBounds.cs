using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PlayerLookState
{
    None = 0,
    LeftWindow = 1,
    SteeringWheel = 2,
    RightWindow = 3,
    Pedals = 4,
    Fridge = 5,
    SunRoof = 6
}

[Serializable]
public struct FrameBounds
{
    [SerializeField]
    public PlayerLookState direction;
    [SerializeField]
    public List<GameObject> bounds;
}
