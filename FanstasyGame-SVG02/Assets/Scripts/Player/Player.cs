using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player ins;

    Rigidbody2D rb;
    Animator anim;
    UIManager ui;


    //Level Player
    int levelPlayer;
    float expCurrent = 0f;

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
    public int dameEnemy = 100;

    public float attackRate = 0.5f;
    float nextAttackTime = 0f;

    //Attack ChestTx
    public Transform AttackPointChestTx;
    float PointRadiusChestTx = 0.01f;
    public LayerMask ChestTxLayer;
    bool hitChestTX;
    bool checkOnChestTx;

    //Heal Player and Power
    float maxHealth;
    float currentHealth;

    float maxPower;
    float currentPower;


    //Time Stop Screen
    float timer;
    float timeDuration;

    //Sound Player
    AudioSource aus;
    public AudioClip soundRun1;
    public AudioClip soundRun2;
    public AudioClip soundDash;
    public AudioClip soundGetItem;
    public AudioClip soundAttack1;
    public AudioClip soundAttack2;
    public AudioClip soundHurt;
    public AudioClip soundDead;
    

    //Create Slider bar
    public Slider sliderHealth;
    public Slider sliderPower;
    void Start()
    {
        ins = this;

        aus = GetComponent<AudioSource>();
        ui = FindObjectOfType<UIManager>();

        timeDuration = 3f;
        timer = timeDuration;

        maxHealth = 100f;
        currentHealth = maxHealth;
        sliderHealth.maxValue = maxHealth;
        sliderHealth.value = maxHealth;

        maxPower = 100f;
        currentPower = maxPower;
        sliderPower.maxValue = maxPower;
        sliderPower.value = maxPower;

        levelPlayer = 1;

        ui.setLevelPlayer("" +levelPlayer);

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
        TimeDead();
        getLevelPlayer();
        attackChextTx();
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
                        aus.PlayOneShot(soundDash);
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
                        aus.PlayOneShot(soundDash);
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

        checkOnChestTx = Physics2D.OverlapCircle(checkGround.position, CheckGroundradius, ChestTxLayer);
        isCheckGrounded = Physics2D.OverlapCircle(checkGround.position, CheckGroundradius, groundLayer);
        if (isCheckGrounded || checkOnChestTx)
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
            enemy.GetComponent<Enemy>().ReceiveDame(dameEnemy);
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

    //Attack ChestTx
    public void attackChextTx()
    {
        hitChestTX = Physics2D.OverlapCircle(AttackPointChestTx.position, PointRadiusChestTx, ChestTxLayer);
        if (hitChestTX)
        {
            ChestTx.ins.openChestTX(1);
            Debug.Log("Chest");
        }
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

    //Sound Run
    public void soundRunLeft()
    {
        aus.PlayOneShot(soundRun1);
    }
    public void soundRunRight()
    {
        aus.PlayOneShot(soundRun2);
    }
    //Sound Attack
    public void soundAttackFirst()
    {
        aus.PlayOneShot(soundAttack1);
    }
    public void soundAttackSecond()
    {
        aus.PlayOneShot(soundAttack2);
    }




    //Recieve Dame
    public void receiveDame(int dame)
    {
        if (anim.GetBool("PlayerDead")) return;

        currentHealth -= dame;
        sliderHealth.value = currentHealth;
        aus.PlayOneShot(soundHurt);
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
        aus.PlayOneShot(soundHurt);
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
        aus.Stop();         
        aus.PlayOneShot(soundDead);
        anim.SetBool("PlayerDead", true);
        Destroy(gameObject, 5f);
    }

    public void TimeDead()
    {
        if (currentHealth <= 0)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0)
            {
                Time.timeScale = 0f;
            }
        }

    }

    //Get mana
    public void Getmana(int receiveMana)
    {
        aus.PlayOneShot(soundGetItem);
        currentPower += receiveMana;
        sliderPower.value = currentPower;
        if (currentPower >= 100)
        {
            currentPower = 100f;
        }
    }

    public void GetHealth(int receiveHealth)
    {
        aus.PlayOneShot(soundGetItem);
        currentHealth += receiveHealth;
        sliderHealth.value = currentHealth;
        if (currentHealth >= 100)
        {
            currentHealth = 100f;
        }
    }


    //Set level Player
    public void getLevelPlayer()
    {
        if (levelPlayer == 1 && expCurrent >= 100)
        {
            levelPlayer = 2;
            ui.setLevelPlayer(""+ levelPlayer);
            expCurrent = 0f;
        }
        else if (levelPlayer == 2 && expCurrent >= 150)
        {
            levelPlayer = 3;
            ui.setLevelPlayer("" + levelPlayer);
            expCurrent = 0f;
        }
        else if (levelPlayer == 3 && expCurrent >= 200)
        {
            levelPlayer = 4;
            ui.setLevelPlayer("" + levelPlayer);
            expCurrent = 0f;

        }
        
    }


    //Receive Exp
    public void takeExp(int exp)
    {
        expCurrent += exp;
        Debug.Log("Exp"+expCurrent);
    }
}
