using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMovement : MonoBehaviour
{
    public GameObject assos_player;
    
    private float allowed_distance = 2f;
    public float follow_speed = 3;
    public RaycastHit[] hits;

    private Rigidbody rb;
    private float target_distance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<OrbManager>().canMove)
        {
            return;
        }
        var heading = assos_player.transform.position - transform.position;
        var direction = heading / heading.magnitude;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(direction));
        for(int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if(hit.collider.gameObject == assos_player)
            {
                target_distance = hit.distance;
                break;
            }
        }
        if(target_distance >= allowed_distance)
        {
            rb.velocity = follow_speed * direction;

        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
