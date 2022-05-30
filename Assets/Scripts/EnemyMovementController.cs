using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] List<WaypointController> path = new List<WaypointController>();
    [SerializeField] private float waitTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        print("Initializing wayfinding");
        StartCoroutine(MoveToWaypoint());
        print("Finished Start method");
    }
    
    IEnumerator MoveToWaypoint()
    {
        foreach( WaypointController waypoint in path)
        {
            print(waypoint.name);
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(waitTime);
        }

    }
}
