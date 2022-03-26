using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    protected float maxHealth;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        maxHealth = 100f;
        currentHealth = maxHealth;

        anim.SetBool("DeadEnemy", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDame(int dame)
    {
        currentHealth -= dame;
        anim.SetTrigger("HurtEnemy");
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            EnemyDead();
        }
    }
    public void EnemyDead()
    {
        anim.SetBool("DeadEnemy", true);

        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 5f);
    }
}
