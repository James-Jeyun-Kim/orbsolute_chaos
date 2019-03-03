using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{

    private PlayerManager PM;
    private OrbManager OM;
    private Animator animator;

    public float knockbackSpeed = 7f;

    private float knockbackTime = 0.3f;
    
    private float invulnTime = 0.3f;

    void Start()
    {
        PM = GetComponent<PlayerManager>();
        OM = GetComponent<OrbManager>();
        animator = GetComponent<Animator>();
    }


    public IEnumerator Knockback(Vector3 direction)
    {
        if (PM != null)
        {
            PM.canMove = false;
            PM.canAttack = false;
            PM.canShootBird = false;
            gameObject.GetComponent<Animator>().SetBool("gotHit", true);
            animator.SetBool("sword_attack", false);
            animator.SetBool("bird_attack", false);
        }
        if (OM != null)
        {
            OM.canMove = false;
        }
        this.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        for (float t = 0; t < knockbackTime; t += Time.deltaTime)
        {
            this.GetComponent<Rigidbody>().velocity = direction * knockbackSpeed;
            yield return new WaitForFixedUpdate();
        }
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if (PM != null)
        {
            PM.canMove = true;
            PM.canAttack = true;
            PM.canShootBird = true;
            gameObject.GetComponent<Animator>().SetBool("gotHit", false);
        }
        if (OM != null)
        {
            OM.canMove = true;
            
        }
    }

    public IEnumerator Invincibility()
    {
        if(PM != null)
        {
            PM.invulnerable = true;
        }
        else
        {
            OM.invulnerable = true;
        }
        for (float endTime = Time.time + invulnTime; Time.time < endTime;)
        {
            if (OM != null)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(.1f);
            if (OM != null)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(.1f);
        }
        if (PM != null)
        {
            PM.invulnerable = false;
        }
        else
        {
            OM.invulnerable = false;
        }
    }
}