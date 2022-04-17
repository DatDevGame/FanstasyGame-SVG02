using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGem : MonoBehaviour
{
    // Start is called before the first frame update
    float randSecond;
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
            ManagerScoreItem.ins.ReceiveGem(1);
            Destroy(gameObject);

        }
    }
}
