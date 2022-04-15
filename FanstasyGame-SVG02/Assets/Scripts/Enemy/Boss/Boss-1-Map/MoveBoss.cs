using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    public static MoveBoss ins;

    Animator anim;
    
    public Transform target;
    float distance;
    float moveSpeed;
    public bool facingRight;
    public bool NotFaceWhenAttack;

    Vector2 moveLeft;
    Vector2 moveRight;

    Vector3 flipBoss;

    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        anim = GetComponent<Animator>();
        moveSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {

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
        Debug.Log(distance);
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
