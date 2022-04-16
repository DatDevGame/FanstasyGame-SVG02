using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolZoneBoss : MonoBehaviour
{
    public static PatrolZoneBoss ins;

    public Transform posBoxCast;
    public LayerMask layer;
    public bool checkPlayerInZone;
    [SerializeField] private float xBoxCast;
    [SerializeField] private float yBoxCast;


    // Start is called before the first frame update
    void Start()
    {
        ins = this;   
    }

    // Update is called once per frame
    void Update()
    {
        boxCast();
    }

    public void boxCast()
    {
        RaycastHit2D hit = Physics2D.BoxCast(posBoxCast.position, new Vector2(xBoxCast, yBoxCast), 0f, Vector2.right, 0f, layer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                checkPlayerInZone = true;
            }
        }
        else 
        {
            checkPlayerInZone = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(posBoxCast.position, new Vector2(xBoxCast, yBoxCast));
    }

    
}
