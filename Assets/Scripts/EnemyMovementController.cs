using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] List<WaypointController> path = new List<WaypointController>();

    // Start is called before the first frame update
    void Start()
    {
        PrintWaypointName();
    }
    
    void PrintWaypointName()
    {
        foreach( WaypointController waypoint in path)
        {
            print(waypoint.name);
        }
    }
}
