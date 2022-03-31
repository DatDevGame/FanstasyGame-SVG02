using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
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
        anim = GetComponent<Animator>();
    }
    public void TestRaycat()
    {
        RaycastHit2D hit = Physics2D.Raycast(PosRaycat.position, Vector2.left, lengthRaycat, playLayer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                if (Time.time >= nextAttackTime)
                {
                    Debug.Log("Hit Player");
                    anim.SetTrigger("AttackEnemy");
                    nextAttackTime = Time.time + 2f / attackRate;
                }
                Debug.DrawRay(PosRaycat.position, Vector2.left * lengthRaycat, Color.red);
            }
        }
        else if (hit.collider == null)
        {
            Debug.Log("Not Hit");
            Debug.DrawRay(PosRaycat.position, Vector2.left * lengthRaycat, Color.green) ;
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
