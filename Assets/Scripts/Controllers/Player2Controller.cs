using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : Controller
{
    public override KeyCode Left_Button()
    {
        return KeyCode.LeftArrow;
    }

    public override KeyCode Right_Button()
    {
        return KeyCode.RightArrow;
    }

    public override void Spawn()
    {
        throw new System.NotImplementedException();
    }

    public override KeyCode Trap_Button()
    {
        return KeyCode.DownArrow;
    }

    public override KeyCode Up_Button()
    {
        return KeyCode.UpArrow;
    }
}
