using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    protected float maxHealth;
    protected float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        maxHealth = 100f;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDame(int dame)
    {
        currentHealth -= dame;
        if (currentHealth <= 0)
        {
            EnemyDead();
        }
    }
    public void EnemyDead()
    {
        Destroy(gameObject);
    }
}
