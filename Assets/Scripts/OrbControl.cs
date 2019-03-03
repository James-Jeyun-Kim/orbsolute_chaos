using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class OrbControl : MonoBehaviour
{
    Rigidbody rb;
    private float movement_speed = 3f;
    private GameObject my_ghost;
    private int controller_num;
    
    public RaycastHit[] hits;
    public GameObject ghost_orb;

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

        controller_num = GetComponent<OrbManager>().assos_player.GetComponent<PlayerManager>().controller_num;
        Gamepad active_gamepad = Gamepad.all[controller_num];

        if(my_ghost != null && Vector3.Distance(transform.position, my_ghost.transform.position) < 0.01)
        {
            Destroy(my_ghost);
        }

        if (!active_gamepad.rightStickButton.isPressed)
        {
            if(my_ghost != null)
            {

                // NEW MECHANIC
                //if (my_ghost.GetComponent<GhostOrbControl>().letGo)
                //{
                //    if (Mathf.Abs(active_gamepad.rightStick.x.ReadValue()) > 0.7 || Mathf.Abs(active_gamepad.rightStick.y.ReadValue()) > 0.7)
                //    {
                //        Destroy(my_ghost);
                //        my_ghost = Instantiate(ghost_orb, transform);
                //        var col = GetComponent<SpriteRenderer>().color;
                //        col.a = 0.5f;
                //        my_ghost.GetComponent<SpriteRenderer>().color = col;
                //        my_ghost.GetComponent<GhostOrbControl>().controller_num = controller_num;
                //        Vector3 b = rb.velocity;
                //        b.x = 0;
                //        b.y = 0;
                //        rb.velocity = b;
                //        return;
                //    }
                //}

                // END OF NEW MECHANIC

                var heading = my_ghost.transform.position - transform.position;
                var direction = heading / heading.magnitude;
                hits = Physics.RaycastAll(transform.position, transform.TransformDirection(direction));
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    if (hit.collider.gameObject == my_ghost)
                    {
                        break;
                    }
                }
                rb.velocity = movement_speed * direction;
                return;
            }
            if(Mathf.Abs(active_gamepad.rightStick.x.ReadValue()) > 0.7 || Mathf.Abs(active_gamepad.rightStick.y.ReadValue()) > 0.7)
            {
                my_ghost = Instantiate(ghost_orb, transform);
                var col = GetComponent<SpriteRenderer>().color;
                col.a = 0.5f;
                my_ghost.GetComponent<SpriteRenderer>().color = col;
                my_ghost.GetComponent<GhostOrbControl>().controller_num = controller_num;
                Vector3 b = rb.velocity;
                b.x = 0;
                b.y = 0;
                rb.velocity = b;
                return;
            }
        }

        if (!GetComponent<OrbManager>().canMove)
        {
            return;
        }


        Vector3 v = rb.velocity;
        
        v.x = active_gamepad.rightStick.x.ReadValue() * movement_speed;
        v.y = active_gamepad.rightStick.y.ReadValue() * movement_speed;

        if (Mathf.Abs(v.x) < 1)
        {
            v.x = 0;
        }
        if (Mathf.Abs(v.y) < 1)
        {
            v.y = 0;
        }

        if (v != Vector3.zero)
        {
            if (my_ghost != null)
            {
                Destroy(my_ghost);
            }
        }



        rb.velocity = v;
    }


}
