using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLadder : MonoBehaviour
{
    public static moveLadder ins;

    Rigidbody2D rb;
    Animator anim;

    public Transform posLadder;
    public LayerMask layer;
    bool checkLadderPlayer;
    public bool setMove;
    [SerializeField] private float radius;
    int checkStop;


    Vector2 velocity;
    private float pressHorizontal;
    public float pressVertical;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        movePlayerLadder();
        checkLadder();
    }


    public void movePlayerLadder()
    {
        pressHorizontal = Input.GetAxis("Horizontal");
        pressVertical = Input.GetAxis("Vertical");
        this.velocity.x = pressHorizontal * moveSpeed * Time.deltaTime;
        this.velocity.y = pressVertical * moveSpeed * Time.deltaTime;

        if (setMove)
        {
            this.rb.MovePosition(this.rb.position + velocity);
        }
    }
    public void checkLadder()
    {
        checkLadderPlayer = Physics2D.OverlapCircle(posLadder.position, radius, layer);
        if (checkLadderPlayer)
        {
            if (pressHorizontal == 0)
            {
                anim.SetFloat("PlayerRuns", -1f);
                anim.SetBool("PlayerLadderStop", true);
                anim.SetBool("PlayerLadder", false);
            }


            if (pressVertical > 0 && Input.GetKey(KeyCode.W))
            {
                anim.SetFloat("PlayerRuns", -1f);
                GetComponent<Rigidbody2D>().gravityScale = 0f;
                setMove = true;
                anim.SetBool("PlayerLadder", true);
                anim.SetBool("PlayerLadderStop", false);
            }
            if (pressVertical < 0 && Input.GetKey(KeyCode.S))
            {
                anim.SetFloat("PlayerRuns", -1f);
                GetComponent<Rigidbody2D>().gravityScale = 0f;
                setMove = true;
                anim.SetBool("PlayerLadder", true);
                anim.SetBool("PlayerLadderStop", false);
            }
        }
        else if (!checkLadderPlayer)
        {
            anim.SetBool("PlayerLadderStop", false);
            anim.SetBool("PlayerLadder", false);
            setMove = false;
            GetComponent<Rigidbody2D>().gravityScale = 2f;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(posLadder.position, radius);
    }

    
}
