using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    //General
    Animator anim;
    AudioSource aus;
    public LayerMask layer;
    int dameBoss;

    //AttackPoins
    public Transform attackPoint;
    [SerializeField]private float radiusAttackPoint;

    //Raycast
    public Transform posRaycast;
    [SerializeField]private float lengthRaycast;

    //Time Attack
    float nextAttack;
    [SerializeField]private float attackRate;

    //Sound Attack
    public AudioClip soundAttack;
    public AudioClip soundAttackInPlayer;

    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        dameBoss = 10;
    }

    // Update is called once per frame
    void Update()
    {
        rayCastAttack();
    }

    public void rayCastAttack()
    {
        if (BossMap1.ins.currentHealthBoss <= 0) return;
        if (Player.ins.currentHealth <= 0) return;
        if (MoveBoss.ins.facingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(posRaycast.position, Vector2.right, lengthRaycast, layer);
            if (hit.collider != null)
            {
                Debug.DrawRay(posRaycast.position, Vector2.right * lengthRaycast, Color.red);
                if (hit.collider.tag == "Player")
                {
                    //Skill Power Full
                    if (BossMap1.ins.powerAttackSkill == 100)
                    {
                        anim.SetTrigger("AttackBoss2");
                        BossMap1.ins.dePower(100);
                        return;
                    }
                    if (Time.time >= nextAttack)
                    {
                        soundAttacks();
                        anim.SetTrigger("AttackBoss1");
                        nextAttack = Time.time + 2f / attackRate;
                    }
                }
            }
            else
            {
                Debug.DrawRay(posRaycast.position, Vector2.right * lengthRaycast, Color.green);
            }
        }
        else if (!MoveBoss.ins.facingRight)
        {
            RaycastHit2D hit = Physics2D.Raycast(posRaycast.position, Vector2.left, lengthRaycast, layer);
            if (hit.collider != null)
            {
                Debug.DrawRay(posRaycast.position, Vector2.left * lengthRaycast, Color.red);
                if (hit.collider.tag == "Player")
                {
                    //Skill Power Full
                    if (BossMap1.ins.powerAttackSkill == 100)
                    {
                        anim.SetTrigger("AttackBoss2");
                        BossMap1.ins.dePower(100);
                        return;
                    }
                    if (Time.time >= nextAttack)
                    {
                        soundAttacks();
                        anim.SetTrigger("AttackBoss1");
                        nextAttack = Time.time + 2f / attackRate;
                    }
                }
            }
            else
            {
                Debug.DrawRay(posRaycast.position, Vector2.left * lengthRaycast, Color.green);
            }

        }
    }
    public void attackBoss1()
    {
        Collider2D[] hitAttack = Physics2D.OverlapCircleAll(attackPoint.position, radiusAttackPoint, layer);
        foreach (Collider2D hitPlayer in hitAttack)
        {
            aus.PlayOneShot(soundAttackInPlayer);
            hitPlayer.GetComponent<Player>().receiveDame(dameBoss);
            BossMap1.ins.setPower(10);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radiusAttackPoint);
    }

    public void soundAttacks()
    {
        aus.PlayOneShot(soundAttack);
    }

}
