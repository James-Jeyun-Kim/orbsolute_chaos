using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class JoystickMovement : MonoBehaviour
{
    Rigidbody rb;
    public float movement_speed = 4;
    public float jump_speed = 5;
    public AudioClip dashSound;
    public AudioClip jumpSound;

    private AudioSource audioSource;
    private int controller_num;
    private Animator animator;
    private int curr_jumps = 0;
    private bool canMove;
    private float dashSpeed = 6f;
    private float dashTime = 0.3f;
    private bool canDash = true;

    // Start is called before the first frame update
    void Start()
    {
        
        controller_num = GetComponent<PlayerManager>().controller_num;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver)
        {
            animator.speed = 0f;
            rb.velocity = Vector3.zero;
            return;
        }

        //if (!GameController.instance.canMove)
        //{
        //    return;
        //}
        canMove = GetComponent<PlayerManager>().canMove;

        if (!canMove)
        {
            return;
        }


        Gamepad active_gamepad = Gamepad.all[controller_num];
        
        // DASH MECHANIC


        if ((active_gamepad.rightTrigger.wasPressedThisFrame || active_gamepad.leftTrigger.wasPressedThisFrame) && GetComponent<PlayerManager>().canAttack && canDash)
        {
            audioSource.PlayOneShot(dashSound);
            StartCoroutine(MakeDash());
            return;
        }



        // END OF DASH MECHANIC

        Vector3 v = rb.velocity;

        // IF LEFT STICK IS CLICKED, FREEZE THE PLAYER
        // DELETE THIS BLOCK TO REMOVE LEFT STICK ORB CONTROL

        //if (active_gamepad.leftStickButton.isPressed)
        //{
        //    v.x = 0;
        //    rb.velocity = v;
        //    animator.SetInteger("move_x", 0);
        //    return;
        //}

        // END OF PLAYER FREEZE

        v.x = active_gamepad.leftStick.x.ReadValue() * movement_speed;
        if ((active_gamepad.aButton.wasPressedThisFrame || active_gamepad.leftStickButton.wasPressedThisFrame) && curr_jumps < 1)
        {
            audioSource.PlayOneShot(jumpSound);
            v.y = jump_speed;
            curr_jumps++;
        }
        rb.velocity = v;
        if (rb.velocity.x > 1)
        {
            if (!GetComponent<PlayerManager>().faceRight)
            {
                transform.Rotate(Vector3.up * 180);
            }
            GetComponent<PlayerManager>().faceRight = true;
            animator.SetInteger("move_x", -1);
        }
        else if (rb.velocity.x < -1)
        {
            if (GetComponent<PlayerManager>().faceRight)
            {
                transform.Rotate(Vector3.up * 180);
            }
            GetComponent<PlayerManager>().faceRight = false;
            animator.SetInteger("move_x", 1);
        }
        else
        {
            v.x = 0;
            rb.velocity = v;
            animator.SetInteger("move_x", 0);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Player"))
        //{
        //    curr_jumps = 0;
        //}
        curr_jumps = 0;
    }

    IEnumerator MakeDash()
    {
        GetComponent<PlayerManager>().canMove = false;
        Vector3 direction;
        if (GetComponent<PlayerManager>().faceRight)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;
        }
        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        canDash = false;
        animator.SetBool("dash", true);
        for (float t = 0; t < dashTime; t += Time.deltaTime)
        {
            rb.velocity = direction * dashSpeed;
            yield return new WaitForFixedUpdate();
        }
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<PlayerManager>().canMove = true;
        animator.SetBool("dash", false);

        for (float t = 0; t < 0.5f; t += Time.deltaTime)
        {
            yield return null;
        }
        canDash = true;
    }


}
