using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMap1 : MonoBehaviour
{
    public static BossMap1 ins;

    AudioSource aus;
    Animator anim;
    Rigidbody2D rb;

    //sound
    public AudioClip sounDead;

    public float currentHealthBoss;
    float maxHealthBoss;

    public float powerAttackSkill;

    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        ins = this;
        maxHealthBoss = 500f;
        currentHealthBoss = maxHealthBoss;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDameBoss(int dame)
    {
        currentHealthBoss -= dame;
        if (currentHealthBoss <= 0)
        {
            DeadBoss();
        }
        Debug.Log("HealthBoss" + currentHealthBoss);
    }
    public void DeadBoss()
    {
        aus.PlayOneShot(sounDead);
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetBool("DeadBoss1", true);
        Destroy(gameObject, 5f);
    }

    public void setPower(int power)
    {
        powerAttackSkill += power;
        if (powerAttackSkill > 100)
        {
            powerAttackSkill = 100;
        }
    }
    public void dePower(int power)
    {
        powerAttackSkill -= power;
    }
}
