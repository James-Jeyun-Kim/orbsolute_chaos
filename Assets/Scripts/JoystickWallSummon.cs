using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class JoystickWallSummon : MonoBehaviour
{
    public GameObject wallPrefab;
    public AudioClip wallSummonSound;

    private AudioSource audioSource;
    private Rigidbody rb;
    private GameObject newWall;
    private Animator animator;
    private PlayerManager PM;
    // Start is called before the first frame update
    void Start()
    {
        PM = GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver)
        {
            return;
        }

        Gamepad active_gamepad = Gamepad.all[PM.controller_num];
        if ((active_gamepad.rightShoulder.wasPressedThisFrame || active_gamepad.leftShoulder.wasPressedThisFrame) && PM.canMove && PM.canAttack && PM.canMakeWall)
        {
            audioSource.PlayOneShot(wallSummonSound);
            PM.canMakeWall = false;
            PM.canMove = false;
            PM.canAttack = false;
            rb.velocity = Vector3.zero;
            SummonWall();
            animator.SetBool("wall_summon", true);
        }
    }

    void SummonWall()
    {
        Vector3 spawnLoc = this.gameObject.transform.position;
        if (PM.faceRight)
        {
            spawnLoc.x += 0.5f;
        }
        else
        {
            spawnLoc.x -= 0.5f;
        }
        newWall = Instantiate(wallPrefab, spawnLoc, transform.rotation);
        newWall.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
        StartCoroutine(WallLife());
    }

    IEnumerator WallLife()
    {
        yield return StartCoroutine(pauseForWall());
        yield return new WaitForSeconds(0.7f);
        PM.canMakeWall = true;
        Destroy(newWall);
    }

    IEnumerator pauseForWall()
    {
        yield return new WaitForSeconds(2f);
    }

    void SummonDone()
    {
        PM.canMove = true;
        PM.canAttack = true;
        animator.SetBool("wall_summon", false);
    }
}
