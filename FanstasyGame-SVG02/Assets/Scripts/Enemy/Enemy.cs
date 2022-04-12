using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static Enemy ins;


    Rigidbody2D rb;
    Animator anim;

    //Sound Enemy
    

    //take EXP for Player
    int expPlayer = 10;

    //Create Health Enemy
    protected float maxHealth;
    public float currentHealth;


    //Enemy Movement
    public Transform target;
    public float speedMove = 5f;
    Vector2 velocity = new Vector2();
    public float distanceAttack;
    bool facingRight;

    //Stop move when Enemy Attack
    bool stopMoveEnemy;

    //Create Item when Enemy Dead
    public GameObject PrefabsItems;
    public Transform PosSpawn;

    //Create Item Mana When Enemty Dead
    public GameObject PrefabsItemMana;
    public Transform PosSpawnMana;

    //Random Create Item When Dead
    int randomItem;


    //Create Item health
    public GameObject PrefabsItemHealth;
    public Transform PosSpawnHealth;

    void Start()
    {
        ins = this;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 100f;
        currentHealth = maxHealth;

        anim.SetBool("DeadEnemy", false);
    }

    // Update is called once per frame
    void Update()
    {
        randomItem = Random.Range(1, 10);
    }
    private void FixedUpdate()
    {
        EnemyMove();
    }

    public void ReceiveDame(int dame)
    {
        if (anim.GetBool("DeadEnemy")) return;
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
        //Random Drop Item
        if (randomItem == 1)
        {
            Instantiate(PrefabsItemHealth, PosSpawnHealth.position, Quaternion.identity);
        }
        else if (randomItem == 5)
        {
            Instantiate(PrefabsItemMana, PosSpawnMana.position, Quaternion.identity);
        }

        else if (randomItem == 9)
        {
            Instantiate(PrefabsItemHealth, PosSpawnHealth.position, Quaternion.identity);
            Instantiate(PrefabsItemMana, PosSpawnMana.position, Quaternion.identity);
            Instantiate(PrefabsItems, PosSpawn.position, Quaternion.identity);
        }

        Instantiate(PrefabsItems, PosSpawn.position, Quaternion.identity);


        takeExpPlayer();

        anim.SetBool("DeadEnemy", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 5f);
    }

    //Check Distance Attack
    public void EnemyMove()
    {
        if (FollowThePath.ins.setBoolOntheGround == false) return;

        if (anim.GetBool("DeadEnemy")) return;
        if (Player.ins.currentHealth <= 0) anim.SetBool("canWalk", false);
        if (Player.ins.currentHealth <= 0) return;

        this.distanceAttack = Vector2.Distance(target.position, transform.position);
        if (distanceAttack <= 5)
        {
            
            if (target.position.x < transform.position.x)
            {
                if (distanceAttack <= 1)
                {
                    anim.SetBool("canWalk", false);
                    return;
                }
                    
                if (stopMoveEnemy) return;
                anim.SetBool("canWalk", true);
                transform.localScale = new Vector3(3f, 3f, 3f);
                this.velocity = Vector2.left * speedMove * Time.deltaTime;
                transform.Translate(velocity);
                setFacingRight();

            }
            else
            {
                if (stopMoveEnemy) return;
                anim.SetBool("canWalk", true);
                transform.localScale = new Vector3(-3f, 3f, 3f);
                this.velocity = Vector2.right * speedMove * Time.deltaTime;
                transform.Translate(velocity);
                setFacingRight();


            }

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


    public void takeExpPlayer()
    {
        Player.ins.takeExp(expPlayer);
    }

     public void setStopMoveEnemyFalse()
    {
        stopMoveEnemy = false;
    }
    public void setStopMoveEnemyTrue()
    {
        stopMoveEnemy = true;
    }

    




}
