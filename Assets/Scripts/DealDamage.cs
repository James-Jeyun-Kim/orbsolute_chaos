using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public GameObject enemyOrb;
    public GameObject ownOrb;
    public GameObject assos_player;

    private Vector3 knockDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(tag == "Sword")
        {
            RaycastHit hit;
            Vector3 originPos = transform.position;
            originPos.x += 0.4f;
            originPos.y += 0.15f;
            if (Physics.Raycast(originPos, Vector3.left, out hit, 1f))
            {
                if (hit.collider.gameObject.CompareTag("Shield"))
                {
                    return;
                }
            }
            originPos.y -= 0.3f;
            if (Physics.Raycast(originPos, Vector3.left, out hit, 1f))
            {
                if (hit.collider.gameObject.CompareTag("Shield"))
                {
                    return;
                }
            }
        }
        if (tag == "Sword")
        {
            if (transform.parent.position.x > other.gameObject.transform.position.x)
            {
                knockDir = new Vector3(-0.5f, 0.5f, 0);
            }
            else
            {
                knockDir = new Vector3(0.5f, 0.5f, 0);
            }
        }
        else if(tag == "Bird")
        {
            if (transform.position.x > other.gameObject.transform.position.x)
            {
                knockDir = new Vector3(-0.5f, 0.5f, 0);
            }
            else
            {
                knockDir = new Vector3(0.5f, 0.5f, 0);
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject == assos_player || other.gameObject.GetComponent<PlayerManager>().invulnerable)
            {
                return;
            }
            other.gameObject.GetComponent<PlayerManager>().TakeDamage(-1, knockDir);
        }
        if (other.gameObject == enemyOrb && !enemyOrb.GetComponent<OrbManager>().invulnerable)
        {
            other.gameObject.GetComponent<OrbManager>().TakeDamage(-3, knockDir);
        }
        //if (other.gameObject == ownOrb)
        //{
        //    other.gameObject.GetComponent<OrbManager>().TakeDamage(1, Vector3.zero);
        //}
    }
}
