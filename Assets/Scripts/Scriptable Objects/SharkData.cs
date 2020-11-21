using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharkData", menuName = "Shark Data", order = 52)]
public class SharkData : ScriptableObject
{
    public float maxSpeed;
    public float maxSpeedTime;
    public float maxSpeedStopTime;
    public float TurnSpeed;
    public float bombCooldownTime;
}
