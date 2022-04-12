using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform target;
    public Transform posRaycast;
    //Prefab Arrow
    public Transform posArrow;

    public float distance;

    float nextAttack;
    public float attackRate = 2f;

    Animator anim;

    public float currentHealth;
    float maxHealth;
    void Start()
    {
        anim = GetComponent<Animator>();
        attackRate = 0.5f;

        maxHealth = 10f;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        attackTarget();
    }

    public void attackTarget()
    {

        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance <= 10)
        {
            if (Time.time >= nextAttack)
            {
                if (distance > 10) return;
                anim.SetTrigger("AttackEnemyArrow");
                nextAttack = Time.time + 2f / attackRate;
            }

            #region Flip
            Vector3 rotation = transform.eulerAngles;
            if (transform.position.x > target.position.x)
            {
                rotation.y = 180f;
            }
            else if (transform.position.x < target.position.x)
            {
                rotation.y = 0f;
            }
            transform.eulerAngles = rotation;
            #endregion
        }
        else
        {
            return;
        }
    }

    public void setSpawnArrow()
    {
        Instantiate(arrowPrefab, posArrow.position, posArrow.rotation);
    }


    public void receiDame(int dame)
    {
        anim.SetTrigger("HurtEnemyArrow");
        currentHealth -= dame;
        if (currentHealth <= 0)
        {
            dead();
        }
    }
    public void dead()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        anim.SetBool("DeadEnemytArrow", true);
        Destroy(gameObject, 4f);
    }
}
