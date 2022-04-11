using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrap : MonoBehaviour
{

    public Transform attackPoint;
    public float radiusAttack;
    public LayerMask layer;

    public int dameEnemyTrap;


    public float currentHealth;
    float maxHealth;


    //Raycast
    public Transform posRaycast;
    public float lengthRaycast = 1.5f;
    float directionRaycast;
    //Check Player Near
    public Transform target;
    float nextAttack;
    public float attackRate = 2f;

    float distance;
    float speedMove = 4f;

    bool stopWhenAttack;


    //BoxCat
    public Transform BoxCast;
    [SerializeField]private float posXBox;
    [SerializeField]private float posYBox;





    //Animation Attack
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 1f;
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        facingTarget();
        raycast();
    }

    public void enemyTrapAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, radiusAttack, layer);

        foreach (Collider2D hit in hitPlayer)
        {
            hit.GetComponent<Player>().receiveDame(dameEnemyTrap);

        }
    }
    private void OnDrawGizmos()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawSphere(attackPoint.position, radiusAttack);
    }

    public void raycast()
    {
        if (Player.ins.currentHealth <= 0) return;
        if (transform.localScale.x > 0)
        {
            directionRaycast = 1f;
        }
        else 
        {
            directionRaycast = -1f;
        }

        RaycastHit2D hit = Physics2D.Raycast(posRaycast.position, Vector2.left * directionRaycast, lengthRaycast, layer);
        if (hit.collider != null)
        {
            Debug.DrawRay(posRaycast.position, Vector2.left * lengthRaycast * directionRaycast, Color.red);
            if (hit.collider.tag == "Player")
            {
                if (Time.time >= nextAttack)
                {
                    anim.SetTrigger("EnemyTrapAttack");

                    nextAttack = Time.time + 2f / attackRate;
                }
                
            }
        }
        else
        {
            Debug.DrawRay(posRaycast.position, Vector2.left * lengthRaycast * directionRaycast, Color.green);
        }
    }

    public void facingTarget()
    {
        if (stopWhenAttack) return;
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector2(3f, 3f);
        }
        else
        {
            transform.localScale = new Vector2(-3f, 3f);
        }
    }

    public void move()
    {
        distance = Vector2.Distance(transform.position, target.position);
        RaycastHit2D boxCheck = Physics2D.BoxCast(BoxCast.position, new Vector2(posXBox, posYBox), 0f, Vector2.right, 0f, layer);
        if (boxCheck.collider != null)
        {
            if (boxCheck.collider.tag == "Player")
            {
                if (distance <= 1.5f)
                {
                    anim.SetBool("EnemyTrapWalk", false);
                    return;
                }
                if (stopWhenAttack) return;

                transform.position = Vector2.MoveTowards(transform.position, target.position, speedMove * Time.deltaTime);
                anim.SetBool("EnemyTrapWalk", true);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(BoxCast.position, new Vector2(posXBox, posYBox));
    }

    public void receiveDame(int dame)
    {
        currentHealth -= dame;
        if (currentHealth <= 0)
        {
            dead();
        }
    }
    public void dead()
    {
        anim.SetBool("EnemyTrapDead", true);
        Destroy(gameObject, 2f);
    }


    public void setStopMoveWhenAttackTrue()
    {
        stopWhenAttack = true;
    }
    public void setStopMoveWhenAttackfalse()
    {
        stopWhenAttack = false;
    }


}
