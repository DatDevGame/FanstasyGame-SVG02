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

    //Skill tele
    int randomPos;
    float timerTeleSkill;
    float timerDurationTeleSkill;
    int directionTeleSkill;
    bool setStopTeleWhenAttack;


    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        anim = GetComponent<Animator>();
        aus = GetComponent<AudioSource>();
        moveSpeed = 3f;

        timeTeleDuration = 5f;
        timerTele = timeTeleDuration;

        timerDurationTeleSkill = 0.7f;
        timerTeleSkill = timerDurationTeleSkill;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        telePlayer();
        skillTeleport();
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
        if (transform.position.x < 295 || transform.position.x > 378)
        {
            Debug.Log("Return");
            return;
        }

        //Fix tele
        if (flipBoss.y == 180f)
        {
            directionTele = -2;
        }
        else if (flipBoss.y == 0)
        {
            directionTele = 2;
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

    public void skillTeleport()
    {
        if (BossMap1.ins.currentHealthBoss <= 0) return;

        if (BossMap1.ins.currentHealthBoss >= 100) return;

        BossMap1.ins.powerUp.SetActive(true);

        directionTeleSkill = Random.Range(-2, 2);

        if (setStopTeleWhenAttack) return; 
        timerTeleSkill -= Time.deltaTime;
        if (timerTeleSkill <= 0)
        {
            randomPos = Random.Range(1, 10);

            if (randomPos == 1)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 2)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 2f * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 3)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 1f * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 4)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 2.5f * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 5)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 2.8f * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 6)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 3.5f * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 7)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 1.5f * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 8)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 0.5f * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 9)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 1.8f * directionTeleSkill, target.position.y + 3f);
            }
            else if (randomPos == 10)
            {
                anim.SetTrigger("TeleBoss");
                aus.PlayOneShot(soundTele);
                transform.position = new Vector2(target.position.x + 0.7f * directionTeleSkill, target.position.y + 3f);
            }
            if (target.position.x > transform.position.x)
            {
                Vector2 flip = transform.eulerAngles;
                flip.y = 0f;
                transform.eulerAngles = flip;
            }
            else
            {
                Vector2 flip = transform.eulerAngles;
                flip.y = -180f;
                transform.eulerAngles = flip;
            }
            timerTeleSkill = timerDurationTeleSkill;
        }
       
    }


    public void notFaceAttackTrue()
    {
        NotFaceWhenAttack = true;
    }
    public void notFaceWhenAttackFalse()
    {
        NotFaceWhenAttack = false;
    }
    public void setBoolNotTeleWhenAttackTrue()
    {
        setStopTeleWhenAttack = true;
    }
    public void setBoolNotTeleWhenAttackFalse()
    {
        setStopTeleWhenAttack = false;
    }
}
