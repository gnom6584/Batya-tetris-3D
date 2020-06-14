using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : Player.InputHandler
{
    float timer;
    float timer1;
    float delay = 0.2f;
    private void Update()
    {
        timer1 -= Time.deltaTime;
        timer -= Time.deltaTime;
        if (Input.GetButton("Fire4"))
        {
            player.Boost = true;
        }
        else
        {
            player.Boost = false;
        }
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Fire3"))
        {
            player.RotateZ();
        }
        if (Input.GetKeyDown(KeyCode.T) || Input.GetButtonDown("Fire2"))
        {
            player.RotateY();
        }
        if (Input.GetKeyDown(KeyCode.Y) || Input.GetButtonDown("Fire1"))
        {
            player.RotateX();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetAxis("Horizontal") < 0)
        {
            if (timer <= 0)
            {
                timer = delay;
                player.MoveX(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
        {
            if (timer <= 0)
            {
                timer = delay;
                player.MoveX(1);
            }
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            if (timer1 <= 0)
            {
                timer1 = delay;
                player.MoveZ(1);
            }
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            if (timer1 <= 0)
            {
                timer1 = delay;
                player.MoveZ(-1);
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.MoveZ(-1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.MoveZ(1);
        }

        if (Input.GetAxis("Horizontal") == 0)
            timer = 0;

    
        //if (Input.GetAxis("Vertical") > 0)
        //{
        //    player.Boost = true;
        //}
        //else
        //{
        //    player.Boost = false;
        //}
    }
}
