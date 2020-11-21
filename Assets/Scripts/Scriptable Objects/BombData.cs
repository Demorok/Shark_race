using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BombData", menuName = "Bomb Data", order = 53)]
public class BombData : ScriptableObject
{
    public float livingTime;
    public float injureTime;
    public float readiness;
}
