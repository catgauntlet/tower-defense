using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] List<WaypointController> path = new List<WaypointController>();
    [SerializeField] [Range(0f, 5f)] private float movementSpeed = 1.0f;

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
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);
            while(travelPercent < 1)
            {
                travelPercent += (Time.deltaTime * movementSpeed);
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }

    }
}
