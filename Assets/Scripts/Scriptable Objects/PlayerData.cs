using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data", order = 51)]
public class PlayerData : ScriptableObject
{
    public string nickname;
    public KeyCode Up_Button;
    public KeyCode Left_Button;
    public KeyCode Right_Button;
    public KeyCode Trap_Button;
}
