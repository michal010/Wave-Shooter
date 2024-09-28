using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementTypes { PingPongUpAndDown, PingPongLeftAndRight, Circural}

[CreateAssetMenu(fileName = "MovingTargetData", menuName = "ScriptableObjects/new MovingTargetData", order = 4)]
public class MovingTargetData : TargetData
{
    public MovementTypes MovementType;
    public float MovementRadius;
    public float MovementSpeed;
}
