using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    Vector2 velocity = new Vector2();
    int directionMove = 1;
    public float moveSpeed;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        moveWayPoint();
    }

    public void moveWayPoint()
    {
        if (Enemy.ins.distanceAttack <= 5) return;
        this.velocity = Vector2.right * directionMove * moveSpeed * Time.deltaTime;
        transform.Translate(velocity);
        anim.SetBool("canWalk", true);

        if (directionMove == 1)
        {
            transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else
        {
            transform.localScale = new Vector3(-3f, 3f, 3f);
        }
       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wayPoint1"))
        {
            directionMove = 1;
        }
        else if (other.gameObject.CompareTag("wayPoint2"))
        {
            directionMove = -1;
        }
    }
}
