using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTeleport : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ManagerScoreItem.ins.setGem >= 8)
            {
                Invoke("closeGateTele", 2f);
            }
        }
    }

    public void closeGateTele()
    {
        anim.SetBool("CloseGateTeleport", true);
        Destroy(gameObject, 1f);
    }
}
