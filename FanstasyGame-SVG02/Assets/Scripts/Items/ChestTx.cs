using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTx : MonoBehaviour
{
    public static ChestTx ins;

    Animator anim;
    AudioSource aus;
    public AudioClip soundOpenChestTx; 
    public GameObject GemPrefabs;
    public GameObject ManaPrefabs;
    public GameObject HealthPrefabs;
    public Transform posSpawnGem;
    public Transform posSpawnMana;
    public Transform posSpawnHealth;

    public float radius = 1f;
    public LayerMask playerLayer;


    public GameObject ShowpressOchest;


    bool checkOpenOne = true;



    bool openChest;
    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        aus = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        checkOpenOne = true;
    }

    // Update is called once per frame
    void Update()
    {
        circleZoneOpenChest();
    }

    public void openChestTX()
    {
        aus.PlayOneShot(soundOpenChestTx);
        Instantiate(GemPrefabs, posSpawnGem.position, Quaternion.identity);
        Instantiate(ManaPrefabs, posSpawnMana.position, Quaternion.identity);
        Instantiate(HealthPrefabs, posSpawnHealth.position, Quaternion.identity);
        anim.SetTrigger("OpenChestTx");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 5f);
    }

    public void circleZoneOpenChest()
    {
        openChest = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (openChest)
        {
            if (Input.GetKeyDown(KeyCode.O) && checkOpenOne)
            {
                openChestTX();
                checkOpenOne = false;
            }

            ShowpressOchest.SetActive(true);
        }
    }
    
}
