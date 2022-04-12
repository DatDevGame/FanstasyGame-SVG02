using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    public static FollowThePath ins;

    Vector2 velocity = new Vector2();
    int directionMove = 1;
    public float moveSpeed;
    Animator anim;

    float distance;
    public Transform target;


    //Check On Ground
    public LayerMask groundLayer;
    public Transform CheckOnTheGround;
    public Transform PosCheckGroundRightLeft;
    float raycastLength = 0.5f;
    float raycastLengthRightLeft = 0.3f;
    public bool setBoolOntheGround;

   

    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        anim = GetComponent<Animator>();

        moveSpeed = 0.5f;
        setBoolOntheGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        setRaycastCheckOneTheGround();
        checkGround();
    }
    private void FixedUpdate()
    {
        moveWayPoint();
    }

    public void moveWayPoint()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance <= 5) return;
        if (Player.ins.currentHealth <= 0) return;

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

    public void setRaycastCheckOneTheGround()
    {
        RaycastHit2D CheckOntheGrounded = Physics2D.Raycast(CheckOnTheGround.position, Vector2.down, raycastLength, groundLayer);
       

        if (CheckOntheGrounded.collider != null)
        {
            setBoolOntheGround = true;
            Debug.DrawRay(CheckOnTheGround.position, Vector2.down * raycastLength, Color.red);
            return;
        }
        else if(CheckOntheGrounded.collider == null)
        {
            setBoolOntheGround = false;
            Debug.DrawRay(CheckOnTheGround.position, Vector2.down * raycastLength, Color.green);
            if (transform.localScale.x < 0)
            {
                directionMove = 1;
            }
            else if (transform.localScale.x > 0)
            {
                directionMove = -1;
            }
        }

       
    }

    public void checkGround()
    {
        RaycastHit2D CheckGroundRightLeft = Physics2D.Raycast(PosCheckGroundRightLeft.position, Vector2.right, raycastLengthRightLeft, groundLayer);
        //CheckGroundRightLeft
        if (CheckGroundRightLeft.collider != null)
        {
            Debug.DrawRay(PosCheckGroundRightLeft.position, Vector2.right * raycastLength, Color.red);
            if (transform.localScale.x < 0)
            {
                directionMove = 1;
            }
            else if (transform.localScale.x > 0)
            {
                directionMove = -1;
            }
        }
        else if(CheckGroundRightLeft.collider == null)
        {
            Debug.DrawRay(PosCheckGroundRightLeft.position, Vector2.right * raycastLength, Color.green);
        }
    }

}
