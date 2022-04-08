using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMana : MonoBehaviour
{
    int earnMana;
    float randSecond;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 rotate = new Vector3(0, 0, 60);
        transform.Rotate(rotate);
        randSecond = Random.Range(20f, 30f);
        earnMana = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, randSecond);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player.ins.Getmana(earnMana);
            Destroy(gameObject);
        }
    }
}
