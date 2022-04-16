using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBG : MonoBehaviour
{
    AudioSource aus;


    public GameObject chestPrefabs;
    
    // Start is called before the first frame update
    void Start()
    {
        aus = GetComponent<AudioSource>();
        aus.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            aus.Play();
            Instantiate(chestPrefabs, new Vector2(303.6344f, 25.52701f), Quaternion.identity);
            Player.ins.currentHealth = 100f;
            Player.ins.currentPower = 100f;
        }

    }
}
