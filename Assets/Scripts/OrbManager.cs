using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip orbHitSound;
    public GameObject assos_player;
    public bool canMove = true;
    public bool invulnerable = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage, Vector3 knockDir)
    {
        audioSource.PlayOneShot(orbHitSound);
        StartCoroutine(GetComponent<KnockBack>().Knockback(knockDir));
        StartCoroutine(GetComponent<KnockBack>().Invincibility());
        assos_player.GetComponent<PlayerManager>().curr_health = Mathf.Max(assos_player.GetComponent<PlayerManager>().curr_health + damage, 0);
    }
}
