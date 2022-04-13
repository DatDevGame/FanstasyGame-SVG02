using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    float moveSpeed;
    Vector2 velocity;

    public GameObject player;
    float directionMove;
    void Start()
    {
        Destroy(gameObject, 10f);

        moveSpeed = 10f;
        transform.Rotate(0f, 0f, 90f);

        player = GameObject.Find("Player");

        if (player.transform.localScale.x > 0)
        {
            directionMove = 1f;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 1f, transform.localScale.z);
        }
        else if (player.transform.localScale.x < 0)
        {
            directionMove = -1f;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1f, transform.localScale.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector2.down* directionMove * moveSpeed * Time.deltaTime;
        transform.Translate(velocity);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy hit = collision.gameObject.GetComponent<Enemy>();
            hit.ReceiveDame(30);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("EnemyBow"))
        {
            EnemyBow hit = collision.gameObject.GetComponent<EnemyBow>();
            hit.receiDame(50);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("EnemyTrap"))
        {
            EnemyTrap hit = collision.gameObject.GetComponent<EnemyTrap>();
            hit.receiveDame(1);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
