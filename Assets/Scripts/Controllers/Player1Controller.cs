using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : Controller
{
    public override KeyCode Left_Button()
    {
        return KeyCode.A;
    }

    public override KeyCode Right_Button()
    {
        return KeyCode.D;
    }

    public override void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public override KeyCode Trap_Button()
    {
        return KeyCode.S;
    }

    public override KeyCode Up_Button()
    {
        return KeyCode.W;
    }
}
