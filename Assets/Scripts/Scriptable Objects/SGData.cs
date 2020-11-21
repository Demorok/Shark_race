using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SGData", menuName = "SG Data", order = 54)]
public class SGData : ScriptableObject
{
    public float timeBeforeChange;
    public float turnSpeed;
    public float turnTime;
    public float streamPower;
}
