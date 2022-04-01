using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;


    //Create Health Enemy
    protected float maxHealth;
    public float currentHealth;


    //Enemy Movement
    public Transform target;
    public float speedMove;
    Vector2 velocity = new Vector2();
    float distanceAttack;
    bool facingRight;
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
    private void FixedUpdate()
    {
        EnemyMove();
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

    //Check Distance Attack
    public void EnemyMove()
    {
        this.distanceAttack = Vector2.Distance(target.position, transform.position);
        Debug.Log(distanceAttack);
        if (distanceAttack <= 3)
        {
            if (target.position.x < transform.position.x)
            {
                if (distanceAttack <= 1) return;
                anim.SetBool("canWalk", true);
                transform.localScale = new Vector3(3f, 3f, 3f);
                this.velocity = Vector2.left * speedMove * Time.deltaTime;
                transform.Translate(velocity);
                setFacingRight();

            }
            else
            {
               
                if (distanceAttack <= 1) return;
                anim.SetBool("canWalk", true);
                transform.localScale = new Vector3(-3f, 3f, 3f);
                this.velocity = Vector2.right * speedMove * Time.deltaTime;
                transform.Translate(velocity);
                setFacingRight();


            }

        }
        else
        {
            anim.SetBool("canWalk", false);
        }
    }
    public void setFacingRight()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void setStopMoveWhenHurt()
    {
        this.velocity = Vector2.zero;
        transform.Translate(velocity);
    }
}
