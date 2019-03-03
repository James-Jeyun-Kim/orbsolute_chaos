using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip hitSound;
    public int max_health = 5;
    public int curr_health;
    public bool canAttack = true;
    public bool canMove = true;
    public bool canMakeWall = true;
    public bool dead = false;
    public int controller_num;
    public bool canShootBird = true;
    public bool faceRight = true;
    public bool invulnerable = false;
    public GameObject enemyOrb;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        curr_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if(curr_health <= 0)
        {
            dead = true;
        }
    }

    public void TakeDamage(int damage, Vector3 knockDir)
    {
        audioSource.PlayOneShot(hitSound);
        StartCoroutine(GetComponent<KnockBack>().Knockback(knockDir));
        StartCoroutine(GetComponent<KnockBack>().Invincibility());
        curr_health = Mathf.Max(0, curr_health + damage);
    }
}
