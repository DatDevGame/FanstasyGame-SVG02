using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBoss : MonoBehaviour
{

    public GameObject healthPrefabs;
    public GameObject manaPrefabs;

    public Transform PosHeal;
    public Transform ManaHeal;

    int randGift;

    float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 1f;
        randGift = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnGift()
    {
        if (randGift == 1 || randGift == 3)
        {
            Instantiate(healthPrefabs, PosHeal.position, Quaternion.identity);
        }
        else if (randGift == 2 || randGift == 4)
        {
            Instantiate(manaPrefabs, PosHeal.position, Quaternion.identity);
        }
        else
        {
            Instantiate(healthPrefabs, PosHeal.position, Quaternion.identity);
            Instantiate(manaPrefabs, PosHeal.position, Quaternion.identity);
        }
    }

    public void receiveDameGift(int dame)
    {
        health -= dame;
        if (health <= 0)
        {
            spawnGift();
            Destroy(gameObject);
        }
    }
}
