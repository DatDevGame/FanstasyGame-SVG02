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
            anim.SetFloat("PlayerRun", 2);
        }
        else
            anim.SetFloat("PlayerRun", -1);

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            this.rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);
        }

    }
    public void setFacingRight()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
