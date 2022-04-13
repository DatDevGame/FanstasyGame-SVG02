using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemy : MonoBehaviour
{
    public static DistanceEnemy ins;

    public Transform target;

    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        ins = this;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceEnemys();
    }

    public void DistanceEnemys()
    {
        distance = Vector2.Distance(transform.position, target.position);
        Debug.Log(distance);

    }
}
