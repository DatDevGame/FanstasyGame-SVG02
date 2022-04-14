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

    //Spawn Item

    public GameObject HealthItem;
    public GameObject ManaItem;
    public GameObject GemItem;

    public Transform posSpawnHealth;
    public Transform posSpawnMana;
    public Transform posSpawnGem;

    public int randomItem;
    void Start()
    {
        anim = GetComponent<Animator>();
        attackRate = 0.5f;

        maxHealth = 10f;
        currentHealth = maxHealth;

        randomItem = Random.Range(1, 5);

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

        if (randomItem == 1 )
        {
            Instantiate(HealthItem, posSpawnHealth.position, Quaternion.identity);
            Instantiate(GemItem, posSpawnGem.position, Quaternion.identity);
        }
        else if (randomItem == 3)
        {
            Instantiate(ManaItem, posSpawnMana.position, Quaternion.identity);
            Instantiate(GemItem, posSpawnGem.position, Quaternion.identity);
        }
        else if (randomItem == 5)
        {
            Instantiate(HealthItem, posSpawnHealth.position, Quaternion.identity);
            Instantiate(ManaItem, posSpawnMana.position, Quaternion.identity);
            Instantiate(GemItem, posSpawnGem.position, Quaternion.identity);
        }
        else
        {
            Instantiate(GemItem, posSpawnGem.position, Quaternion.identity);
        }

    }
}
