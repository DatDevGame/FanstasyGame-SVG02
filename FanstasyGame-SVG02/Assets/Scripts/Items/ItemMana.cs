using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMana : MonoBehaviour
{
    int earnMana;
    // Start is called before the first frame update
    void Start()
    {
        earnMana = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 30f);
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
