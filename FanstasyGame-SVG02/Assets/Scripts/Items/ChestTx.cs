using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTx : MonoBehaviour
{
    public static ChestTx ins;

    Animator anim;
    AudioSource aus;
    public AudioClip soundOpenChestTx; 


    float currentHealth = 1f;
    public GameObject GemPrefabs;
    public GameObject ManaPrefabs;
    public GameObject HealthPrefabs;
    public Transform posSpawnGem;
    public Transform posSpawnMana;
    public Transform posSpawnHealth;


    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        aus = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openChestTX(int dame)
    {
        currentHealth -= dame;
        if (currentHealth <= 0)
        {
            aus.PlayOneShot(soundOpenChestTx);
            Instantiate(GemPrefabs, posSpawnGem.position, Quaternion.identity);
            Instantiate(ManaPrefabs, posSpawnMana.position, Quaternion.identity);
            Instantiate(HealthPrefabs, posSpawnHealth.position, Quaternion.identity);
            anim.SetTrigger("OpenChestTx");
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 5f);
        }
    }
}
