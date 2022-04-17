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
            randPos = new Vector2(51.78f, 13.78363f);
        }
        else if (randPosSpawn == 2)
        {
            randPos = new Vector2(44.78f, 17.75f);
        }
        else if (randPosSpawn == 3)
        {
            randPos = new Vector2(28.3f, 16.64f);
        }
        else if (randPosSpawn == 4)
        {
            randPos = new Vector2(61.86f, 17.82f);
        }
        else
        {
            randPos = new Vector2(81.51f, 16.81f);
        }
    }
    public void SpawnRandom()
    {
        Instantiate(ChestPrefabs, randPos, Quaternion.identity);
    }

}
