using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public static PatrolEnemy ins;
    //Zone Patrol
    
    public LayerMask playerLayer;
    public bool checkInZonePatrol;
    public Transform Zone;
    [SerializeField] private float boxZone2x;
    [SerializeField] private float boxZone2y;

    public bool checkInZonePatrolZone1;
    public Transform Zone1;
    [SerializeField] private float Zone1boxZone2x;
    [SerializeField] private float Zone1boxZone2y;

    public bool checkInZonePatrolZone2;
    public Transform Zone2;
    [SerializeField] private float Zone2boxZone2x;
    [SerializeField] private float Zone2boxZone2y;
    // Start is called before the first frame update
    void Start()
    {
        ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        checkZonePatrol();
    }
    public void checkZonePatrol()
    {
        RaycastHit2D hit = Physics2D.BoxCast(Zone.position, new Vector2(boxZone2x, boxZone2y), 0f, Vector2.right, 0f, playerLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                checkInZonePatrol = true;
            }
        }
        else if (hit.collider == null)
        {
            checkInZonePatrol = false;
        }

        //Zone 1
        RaycastHit2D hitZone1 = Physics2D.BoxCast(Zone1.position, new Vector2(Zone1boxZone2x, Zone1boxZone2y), 0f, Vector2.right, 0f, playerLayer);
        if (hitZone1.collider != null)
        {
            if (hitZone1.collider.tag == "Player")
            {
                checkInZonePatrolZone1 = true;
            }
        }
        else if (hitZone1.collider == null)
        {
            checkInZonePatrolZone1 = false;
        }

        //Zone 2
        RaycastHit2D hitZone2 = Physics2D.BoxCast(Zone2.position, new Vector2(Zone2boxZone2x, Zone2boxZone2y), 0f, Vector2.right, 0f, playerLayer);
        if (hitZone2.collider != null)
        {
            if (hitZone2.collider.tag == "Player")
            {
                checkInZonePatrolZone2 = true;
            }
        }
        else if (hitZone2.collider == null)
        {
            checkInZonePatrolZone2 = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Zone.position, new Vector2(boxZone2x, boxZone2y));
        Gizmos.DrawWireCube(Zone1.position, new Vector2(Zone1boxZone2x, Zone1boxZone2y));
        Gizmos.DrawWireCube(Zone2.position, new Vector2(Zone2boxZone2x, Zone2boxZone2y));
    }
}
