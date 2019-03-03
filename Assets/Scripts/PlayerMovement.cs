using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float movement_speed = 4;
    public float jump_speed = 8;
    public bool right = true;

    private Animator animator;
    private int curr_jumps = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!GameController.instance.canMove)
        //{
        //    return;
        //}
        Vector3 v = rb.velocity;
        v.x = Input.GetAxisRaw("Horizontal") * movement_speed;
        if (Input.GetKeyDown("space") && curr_jumps < 1)
        {
            v.y = jump_speed;
            curr_jumps++;
        }
        rb.velocity = v;
        if (rb.velocity.x > 1)
        {
            if (!right)
            {
                transform.Rotate(Vector3.up * 180);
            }
            right = true;
            animator.SetInteger("move_x", 1);
        }
        else if (rb.velocity.x == 0)
        {
            animator.SetInteger("move_x", 0);
        }
        else
        {
            if (right)
            {
                transform.Rotate(Vector3.up * 180);
            }
            right = false;
            animator.SetInteger("move_x", -1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Player"))
        {
            curr_jumps = 0;
        }
    }


}
