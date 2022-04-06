using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player ins;

    Rigidbody2D rb;
    Animator anim;

    //Set During Hurt not Move
    bool isNotMove;

    //Player move
    float moveSpeed;
    protected float pressHorizontal;
    Vector2 velocity = new Vector2();
    bool facingRight;

    //Player Dash
    float moveDash = 7f;
    float nextDashTime;
    public float dashRate = 1f;

    //Player jump -- Slide Wall
            //Jump
    public Transform checkGround;
    bool isCheckGrounded;
    float CheckGroundradius = 0.1f;
    float jumpForce;
    Vector2 jump = new Vector2(0f, 1f);
    bool isGrounded;
            //
    public LayerMask groundLayer;
            //
            //Slide Wall
    public Transform checkWallSlide;
    bool isCheckWallSlide;
    bool isTouchingWall;
    float moveSlideWall = -0.4f;
    //Check Jump when Slide wall
    bool Walljumping;
    public float xWallForce;
    public float yWallForce;
    public float timeJumpWall;


    


    //Player Attack
    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    bool stopMoveAttack = false;

    public float attackRate = 0.5f;
    float nextAttackTime = 0f;

    //Heal Player and Power
    float maxHealth;
    float currentHealth;

    float maxPower;
    float currentPower;



    //Create Slider bar
    public Slider sliderHealth;
    public Slider sliderPower;
    void Start()
    {
        ins = this;

        maxHealth = 100f;
        currentHealth = maxHealth;
        sliderHealth.maxValue = maxHealth;
        sliderHealth.value = maxHealth;

        maxPower = 100f;
        currentPower = maxPower;
        sliderPower.maxValue = maxPower;
        sliderPower.value = maxPower;


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
        PlayerDash();
    }
    private void FixedUpdate()
    {
        PlayerMovent();
    }

    //Player Movement

    public void PlayerMovent()
    {
        if (anim.GetBool("PlayerDead")) return;
        if (isNotMove) return;

        if (stopMoveAttack) return;
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

    public void PlayerDash()
    {
        if (anim.GetBool("PlayerDead")) return;
        if (isNotMove) return;

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Time.time >= nextDashTime)
            {
                if (!anim.GetBool("PlayerJump"))
                {
                    if (pressHorizontal < 0)
                    {
                        if (currentPower <= 0) return;
                        currentPower -= 5;
                        sliderPower.value = currentPower;
                        anim.SetFloat("PlayerRuns", -1f);
                        anim.SetBool("PlayerDash", true);
                        this.rb.velocity = Vector2.left * moveDash;
                        nextDashTime = Time.time + 2f / dashRate;
                    }
                    else if (pressHorizontal > 0)
                    {
                        if (currentPower <= 0) return;
                        currentPower -= 5;
                        sliderPower.value = currentPower;
                        anim.SetFloat("PlayerRuns", -1f);
                        anim.SetBool("PlayerDash", true);
                        this.rb.velocity = Vector2.right * moveDash;
                        nextDashTime = Time.time + 2f / dashRate;
                    }
                }
                
                
            }
        }

        if (anim.GetBool("PlayerDash"))
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                anim.SetBool("PlayerDashAttack", true);
                anim.SetBool("PlayerDash", false);
            }
        }
           
    }

    //Player Jump
    public void PlayerJump()
    {
        if (anim.GetBool("PlayerDead")) return;
        if (isNotMove) return;

        isCheckGrounded = Physics2D.OverlapCircle(checkGround.position, CheckGroundradius, groundLayer);
        if (isCheckGrounded)
        {
            isGrounded = true;
            anim.SetBool("PlayerJump", false);
        }
        else 
        {
            isGrounded = false;
            anim.SetBool("PlayerJump", true);
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            this.rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);
        }


        isTouchingWall = Physics2D.OverlapCircle(checkWallSlide.position, CheckGroundradius, groundLayer);
        if (isTouchingWall && !isGrounded && pressHorizontal != 0)
        {
            isCheckWallSlide = true;
            anim.SetBool("PlayerWallJump", true);
        }
        else
        {
            isCheckWallSlide = false;
            anim.SetBool("PlayerWallJump", false);
        }

        if (isCheckWallSlide)
        {
            this.rb.velocity = new Vector2(this.rb.velocity.x, Mathf.Clamp(this.rb.velocity.y, moveSlideWall, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.W) && isCheckWallSlide)
        {
            Walljumping = true;
            Invoke("setBoolWallJumpFalse", timeJumpWall);
        }

        if (Walljumping)
        {
            this.rb.velocity = new Vector2(xWallForce * -pressHorizontal, yWallForce);
        }



        
    }
    public void setBoolWallJumpFalse()
    {
        Walljumping = false;
    }


    //Set facing Player
    public void setFacingRight()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
        if (anim.GetBool("PlayerDead")) return;

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
                    nextAttackTime = Time.time + 2f / attackRate;
                }

            }
        }
    }
    //Set Status Animation
    public void setStopMoveAttack()
    {
        stopMoveAttack = false;
    }
    public void setStartMoveAttack()
    {
        stopMoveAttack = true;
    }
    public void SetBoolDashAttack()
    {
        anim.SetBool("PlayerDashAttack", false);
    }
    public void SetBoolDash()
    {
        anim.SetBool("PlayerDash", false);
    }
    void SetBoolHurt()
    {
        isNotMove = false;
    }

    //Recieve Dame
    public void receiveDame(int dame)
    {
        if (anim.GetBool("PlayerDead")) return;

        currentHealth -= dame;
        sliderHealth.value = currentHealth;
        anim.SetTrigger("PlayerHurt");
        isNotMove = true;
        Invoke("SetBoolHurt", 0.9f);
        if (currentHealth <= 0)
        {
            playerDead();
        }
        
    }


    //Trap Enviroment
    public void ReceiveDameTrap(int dameTrap)
    {
        currentHealth -= dameTrap;
        sliderHealth.value = currentHealth;
        anim.SetTrigger("PlayerHurt");
        isNotMove = true;
        Invoke("SetBoolHurt", 0.9f);
        if (currentHealth <= 0)
        {
            playerDead();

        }
    }


    public void playerDead()
    {
        anim.SetBool("PlayerDead", true);
        Destroy(gameObject, 2);
    }

    //Get mana
    public void Getmana(int receiveMana)
    {
        currentPower += receiveMana;
        sliderPower.value = currentPower;
        if (currentPower >= 100)
        {
            currentPower = 100f;
        }
    }

    public void GetHealth(int receiveHealth)
    {
        currentHealth += receiveHealth;
        sliderHealth.value = currentHealth;
        if (currentHealth >= 100)
        {
            currentHealth = 100f;
        }
    }




}
