using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private bool canAttack;
    private float timeSinceAttack = 0f;
    private float attackPeriod = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        canAttack = GetComponent<PlayerManager>().canAttack;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        if(timeSinceAttack >= attackPeriod)
        {
            canAttack = true;
            timeSinceAttack = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Z) && canAttack)
        {
            canAttack = false;
            GameControl.instance.player1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            animator.SetBool("sword_attack", true);
        }
    }

    void AnimationEnded()
    {
        if(animator != null)
        {
            animator.SetBool("sword_attack", false);
        }
        
    }
}
