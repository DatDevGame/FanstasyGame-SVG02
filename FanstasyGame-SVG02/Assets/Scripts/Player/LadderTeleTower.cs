using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTeleTower : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColiderLadder"))
        {
            transform.position = new Vector3(77, 23f, 0f);
        }

        if (collision.gameObject.CompareTag("ColiderLadder1"))
        {
            transform.position = new Vector3(85.67f, 23f, 0f);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ColiderLadderDown"))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(76.65341f, 21.27972f, 0f);
            }
        }

        if (collision.gameObject.CompareTag("ColiderLadderDown1"))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = new Vector3(85.67f, 21.27972f, 0f);
            }
        }
    }
}
