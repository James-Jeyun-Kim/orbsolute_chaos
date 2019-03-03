using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

// LINK TO CONTROLLER KEY REFERENCES
// https://docs.unity3d.com/Packages/com.unity.inputsystem@0.1/api/UnityEngine.Experimental.Input.Gamepad.html

public class JoystickAttack : MonoBehaviour
{
    public GameObject birdPrefab;
    public AudioClip shootBirdSound;
    public AudioClip attackSound1;
    public AudioClip attackSound2;

    private AudioSource audioSource;
    private int controller_num;
    private Animator animator;
    private Rigidbody rb;
    private PlayerManager PM;
    private GameObject newBird;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        PM = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
        controller_num = PM.controller_num;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver)
        {
            animator.speed = 0f;
            return;
        }


        Gamepad active_gamepad = Gamepad.all[controller_num];
        if (active_gamepad.xButton.wasPressedThisFrame && PM.canAttack && !animator.GetBool("bird_attack"))
        {
            PM.canAttack = false;
            rb.velocity = Vector3.zero;
            if (Random.Range(0, 2) == 0)
            {
                audioSource.PlayOneShot(attackSound1);
            }
            else
            {
                audioSource.PlayOneShot(attackSound2);
            }
            animator.SetBool("sword_attack", true);
        }
        else if (active_gamepad.bButton.wasPressedThisFrame && PM.canAttack && PM.canShootBird && PM.canMove)
        {
            audioSource.PlayOneShot(shootBirdSound);
            PM.canAttack = false;
            rb.velocity = Vector3.zero;
            animator.SetBool("bird_attack", true);
            PM.canMove = false;
        }
    }
    
    void AnimationEnded()
    {
        if(animator != null)
        {
            animator.SetBool("sword_attack", false);
        }
        PM.canAttack = true;
        PM.canMove = true;
    }

    void AnimationEndedBird()
    {
        if (animator != null)
        {
            animator.SetBool("bird_attack", false);
        }
        PM.canAttack = true;
        PM.canMove = true;
    }

    void SpawnBird()
    {
        Vector3 spawnLoc = this.gameObject.transform.position;
        if (PM.faceRight)
        {
            spawnLoc.x += 0.78f;
            spawnLoc.y += -0.1f;
        }
        else
        {
            spawnLoc.x -= 0.78f;
            spawnLoc.y += -0.1f;
        }
        PM.canShootBird = false;
        newBird = Instantiate(birdPrefab, spawnLoc, transform.rotation);
        newBird.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        newBird.GetComponent<Death>().player = this.gameObject;
        newBird.GetComponent<BirdMove>().faceRight = PM.faceRight;
        newBird.GetComponent<BirdMove>().assos_player = this.gameObject;
        newBird.GetComponent<DealDamage>().assos_player = this.gameObject;
        newBird.GetComponent<DealDamage>().enemyOrb = PM.enemyOrb;
        StartCoroutine(BirdLife());
    }

    IEnumerator BirdLife()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(newBird);
    }
}
