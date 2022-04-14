using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform player;
    public GameObject playerPos;
    public GameObject EnemyBow;
    Vector2 targetLeft;
    Vector2 targetRight;
    public float speed;

    Vector3 rotate;



    bool direction;

    Rigidbody2D rb;
    public void Awake()
    {

    }
    void Start()
    {
        speed = 10f;

        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 10f);
        player = GameObject.FindGameObjectWithTag("Player").transform;

        EnemyBow = GameObject.Find("EnemyBow");
        playerPos = GameObject.Find("Player");

        setBool();

        targetLeft = new Vector2(player.position.x, player.position.y);
        targetRight = new Vector2(player.position.x, player.position.y);
        transform.Rotate(rotate);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void move()
    {
        if (this.rb.isKinematic) return;

        if (playerPos.transform.position.x < EnemyBow.transform.position.x && direction)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetRight, speed * Time.deltaTime);
        }
        else if (playerPos.transform.position.x > EnemyBow.transform.position.x && !direction)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetLeft, speed * Time.deltaTime);
        }


        if (transform.position.x == targetLeft.x && transform.position.y == targetLeft.y)
        {
            Destroy(gameObject);
        }
        if (transform.position.x == targetRight.x && transform.position.y == targetRight.y)
        {
            Destroy(gameObject);
        }
    }
    public void setBool()
    {
        if (playerPos.transform.position.x < EnemyBow.transform.position.x)
        {
            direction = true;
        }
        else if (playerPos.transform.position.x > EnemyBow.transform.position.x)
        {
            direction = false;
        }


        if (player.position.y < EnemyBow.transform.position.y)
        {
            rotate = new Vector3(0, 0, player.position.y * -2f);
        }
        else if (player.position.y > EnemyBow.transform.position.y)
        {
            rotate = new Vector3(0, 0, player.position.y / 2);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player hit = other.gameObject.GetComponent<Player>();
            hit.receiveDame(10);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            this.rb.isKinematic = true;
        }
    }

   
}
