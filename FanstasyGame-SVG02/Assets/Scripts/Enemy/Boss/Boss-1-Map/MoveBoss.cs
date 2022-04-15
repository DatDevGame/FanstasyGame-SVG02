using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    public static MoveBoss ins;

    Animator anim;
    AudioSource aus;

    public Transform target;
    float distance;
    float moveSpeed;
    public bool facingRight;
    public bool NotFaceWhenAttack;
    Vector2 moveRight;
    Vector3 flipBoss;


    //Tele
    public AudioClip soundTele;
    float timerTele;
    float timeTeleDuration;
    int directionTele;

    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        anim = GetComponent<Animator>();
        aus = GetComponent<AudioSource>();
        moveSpeed = 3f;

        timeTeleDuration = 5f;
        timerTele = timeTeleDuration;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        telePlayer();
    }

    public void Move()
    {
        if (BossMap1.ins.currentHealthBoss <= 0) return;
        if (!PatrolZoneBoss.ins.checkPlayerInZone) return;
        flipBoss = transform.eulerAngles;
        distance = Vector2.Distance(transform.position, target.position);

        if (distance <= 20)
        {
            if (NotFaceWhenAttack) return;


            if (transform.position.x > target.position.x)
            {
                facingRight = false;
                flipBoss.y = 180f;
                transform.eulerAngles = flipBoss;
            }
            else 
            {
                facingRight = true;
                flipBoss.y = 0f;
                transform.eulerAngles = flipBoss;
            }
            if (distance <= 3.5f)
            {
                anim.SetBool("WalkBoss1", false);
                return;
            }
            anim.SetBool("WalkBoss1", true);
            moveRight = Vector2.right * moveSpeed * Time.deltaTime;
            transform.Translate(moveRight);
        }
    }

    //Tele
    public void telePlayer()
    {
        if (BossMap1.ins.currentHealthBoss <= 0) return;
        //Not bug Tele
        if (transform.position.x < 295 || transform.position.x > 335)
        {
            Debug.Log("Return");
            return;
        }

        //Fix tele
        if (flipBoss.y == 180f)
        {
            directionTele = 3;
        }
        else if (flipBoss.y == 0)
        {
            directionTele = -3;
        }


        if (distance <= 10) return;

        timerTele -= Time.deltaTime;
        if (timerTele <= 0)
        {
            anim.SetTrigger("TeleBoss");
            aus.PlayOneShot(soundTele);
            transform.position = new Vector2(target.position.x + directionTele, target.position.y + 2f);
            timerTele = timeTeleDuration;
        }
        Debug.Log(timerTele);
    }

    public void notFaceAttackTrue()
    {
        NotFaceWhenAttack = true;
    }
    public void notFaceWhenAttackFalse()
    {
        NotFaceWhenAttack = false;
    }
}
