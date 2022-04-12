using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform player;
    public GameObject EnemyBow;
    Vector2 target;
    public float speed;


   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        EnemyBow = GameObject.Find("EnemyBow");
        target = new Vector2(player.position.x, player.position.y);

        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
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
    }

   
}
