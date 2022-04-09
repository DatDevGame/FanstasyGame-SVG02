using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject ChestPrefabs;

    Vector2 randPos;

    int randPosSpawn;
    void Start()
    {
        RandSpawnPos();
        SpawnRandom();
    }
    private void Awake()
    {
        randPosSpawn = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void RandSpawnPos()
    {
        if (randPosSpawn == 1)
        {
            randPos = new Vector2(50.08f, 1.05f);
        }
        else if (randPosSpawn == 2)
        {
            randPos = new Vector2(58.32f, 1.05f);
        }
        else if (randPosSpawn == 3)
        {
            randPos = new Vector2(52.44f, 9.19f);
        }
        else if (randPosSpawn == 4)
        {
            randPos = new Vector2(52.01f, 13.74f);
        }
        else
        {
            randPos = new Vector2(27.61f, 15.12f);
        }
    }
    public void SpawnRandom()
    {
        Instantiate(ChestPrefabs, randPos, Quaternion.identity);
    }

}
