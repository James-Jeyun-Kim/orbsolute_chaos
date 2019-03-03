using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAttack : MonoBehaviour
{
    public AudioClip blockBirdSound;
    public AudioClip blockSwordSound;

    private Animator animator;
    private AudioSource audioSource;
    private float prev_speed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void pauseAnim()
    {
        prev_speed = animator.speed;
        animator.speed = 0f;
        StartCoroutine(buyTime());
        
    }

    IEnumerator buyTime()
    {
        yield return new WaitForSeconds(2f);
        animator.speed = prev_speed;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bird"))
        {
            audioSource.PlayOneShot(blockBirdSound);
            collider.GetComponent<Death>().Die();
        }
        if (collider.CompareTag("Sword"))
        {
            audioSource.PlayOneShot(blockSwordSound);
        }
    }
}
