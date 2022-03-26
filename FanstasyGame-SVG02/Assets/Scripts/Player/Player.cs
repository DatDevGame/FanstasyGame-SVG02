using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    //Player move
    float moveSpeed;
    protected float pressHorizontal;
    Vector2 velocity = new Vector2();
    bool facingRight;

    //Player jump
    float jumpForce;
    Vector2 jump = new Vector2(0f, 1f);
    bool isGrounded;

    //Player Attack
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    void Start()
    {
        moveSpeed = 5f;
        jumpForce = 10f;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackAnimationPlayer();
        PlayerJump();
    }
    private void FixedUpdate()
    {
        PlayerMovent();
    }
    public void PlayerMovent()
    {
        this.pressHorizontal = Input.GetAxis("Horizontal");
        this.velocity.x = pressHorizontal * moveSpeed * Time.deltaTime;

        transform.Translate(velocity);

        
        if (pressHorizontal > 0 || pressHorizontal < 0)
        {
             anim.SetFloat("PlayerRuns", 2);
        }
        else
             anim.SetFloat("PlayerRuns", -1);
        


        if (!isGrounded && anim.GetBool("PlayerJump"))
        {
            anim.SetFloat("PlayerRuns", 0);
        }


        if (pressHorizontal < 0 && !facingRight)
        {
            setFacingRight();
        }
        if (pressHorizontal > 0 && facingRight)
        {
            setFacingRight();
        }
    }
    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded == true)
            {
                anim.SetBool("PlayerJump", true);
                this.rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }
    public void setFacingRight()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    public void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
        anim.SetBool("PlayerJump", false);
    }



    //PlayerAttack
    public void attackPlayers()
    {
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log("Attack Enemy");
            enemy.GetComponent<Enemy>().ReceiveDame(50);
        }
    }
    public void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
    public void AttackAnimationPlayer()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (pressHorizontal < 0 || pressHorizontal > 0)
            {
                return;
            }
            else
            {
                if (Time.time >= nextAttackTime)
                {
                    anim.SetTrigger("PlayerAttack");
                    this.pressHorizontal = 0f;
                    attackPlayers();
                    nextAttackTime = Time.time + 0.5f / attackRange;
                }

            }
        }
    }
}
