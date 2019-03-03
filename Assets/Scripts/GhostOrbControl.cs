using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class GhostOrbControl : MonoBehaviour
{
    Rigidbody rb;
    private float movement_speed = 20f;
    public int controller_num;
    //public bool letGo = false;

    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        Gamepad active_gamepad = Gamepad.all[controller_num];


        Vector3 v = rb.velocity;
        if (!active_gamepad.rightStickButton.isPressed)
        {
            v.x = active_gamepad.rightStick.x.ReadValue() * movement_speed;
            v.y = active_gamepad.rightStick.y.ReadValue() * movement_speed;
        }
        else
        {
            v.x = 0;
            v.y = 0;
        }
        
        if (Mathf.Abs(v.x) < 6)
        {
            v.x = 0;
        }
        if (Mathf.Abs(v.y) < 6)
        {
            v.y = 0;
        }
        //if(v.x == 0 && v.y == 0)
        //{
        //    letGo = true;
        //}

        rb.velocity = v;
    }

}
