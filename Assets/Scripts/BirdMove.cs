using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    public bool faceRight;
    public float moveSpeed = 6f;
    public GameObject assos_player;

    private Rigidbody rb;
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (faceRight)
        {
            rb.velocity = new Vector3(moveSpeed, 0, 0);
        }
        else
        {
            rb.velocity = new Vector3(-1 * moveSpeed, 0, 0);
        }
    }
    

    private void OnDestroy()
    {
        assos_player.GetComponent<PlayerManager>().canShootBird = true;
    }
}
