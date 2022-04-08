using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    Enemy enemy;

    public Transform PosRaycat;
    public LayerMask playLayer;
    [SerializeField] private float lengthRaycat = 5f;

    float nextAttackTime;
    public float attackRate = 2f;

    //Enemy Attack
    public Transform attackPoint;
    public float attackRange;


   

    Animator anim;

    private void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        anim = GetComponent<Animator>();
    }
    public void TestRaycat()
    {
        if (anim.GetBool("DeadEnemy")) return;

        RaycastHit2D hit1 = Physics2D.Raycast(PosRaycat.position, Vector2.left, lengthRaycat, playLayer);
        if (hit1.collider != null)
        {
            if (hit1.collider.tag == "Player")
            {
                if (Time.time >= nextAttackTime)
                {
                    anim.SetTrigger("AttackEnemy");
                    anim.SetBool("canWalk", false);
                    nextAttackTime = Time.time + 2f / attackRate;
                }
                Debug.DrawRay(PosRaycat.position, Vector2.left * lengthRaycat, Color.red);
            }
        }
        else if (hit1.collider == null)
        {
            Debug.DrawRay(PosRaycat.position, Vector2.left * lengthRaycat, Color.green);
        }



        RaycastHit2D hit2 = Physics2D.Raycast(PosRaycat.position, Vector2.right, lengthRaycat, playLayer);
        if (hit2.collider != null)
        {
            if (hit2.collider.tag == "Player")
            {
                if (Time.time >= nextAttackTime)
                {
                    anim.SetTrigger("AttackEnemy");
                    anim.SetBool("canWalk", false);
                    nextAttackTime = Time.time + 2f / attackRate;
                }
                Debug.DrawRay(PosRaycat.position, Vector2.right * lengthRaycat, Color.red);
            }
        }
        else if (hit2.collider == null)
        {
            Debug.DrawRay(PosRaycat.position, Vector2.right * lengthRaycat, Color.green) ;
        }
        
    }
    private void Update()
    {
        TestRaycat();
    }

    //Enemy Attack
    public void EnemyAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playLayer);

        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Hit Player");
            player.GetComponent<Player>().receiveDame(10);
        }   
    }
    public void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
