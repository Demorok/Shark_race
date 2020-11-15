using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract void Spawn();
    public abstract KeyCode Up_Button();
    public abstract KeyCode Left_Button();
    public abstract KeyCode Right_Button();
    public abstract KeyCode Trap_Button();
}
