using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePathEnemy : MonoBehaviour
{
    public Transform target;

    float distance;
    bool confirmLocation;

    int directionMove;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance >= 4.5f && confirmLocation == true)
        {
            Debug.Log(distance);
        }
    }
}
