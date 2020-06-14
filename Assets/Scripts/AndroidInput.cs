using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidInput : Player.InputHandler
{
    public void ButtonLeft()
    {
        player.MoveX(-1);
    }

    public void ButtonRight()
    {
        player.MoveX(1);
    }

    public void ButtonRotate()
    {
        player.RotateZ();
    }
}
