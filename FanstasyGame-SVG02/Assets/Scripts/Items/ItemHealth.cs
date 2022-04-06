using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : MonoBehaviour
{

    int healthReceive = 10;
    float randSecond;
    // Start is called before the first frame update
    void Start()
    {
        randSecond = Random.Range(20f, 30f);
        Destroy(gameObject, randSecond);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.ins.GetHealth(healthReceive);
            Destroy(gameObject);
        }
    }
}
