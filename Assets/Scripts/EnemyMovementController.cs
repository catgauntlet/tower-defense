using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField] List<WaypointController> path = new List<WaypointController>();
    [SerializeField] [Range(0f, 5f)] private float movementSpeed = 1.0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        print("Initializing wayfinding");
        FindPath();
        ReturnToStart();
        StartCoroutine(MoveToWaypoint());
        print("Finished Start method");
    }
    
    void FindPath()
    {
        path.Clear();

        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform waypoint in pathParent.transform) {
            path.Add(waypoint.GetComponent<WaypointController>());
        }
    }

    void ReturnToStart()
    {
      transform.position = path[0].transform.position;
    }

    void ReachedEndOfPath()
    {
        gameObject.SetActive(false);
    }

    IEnumerator MoveToWaypoint()
    {
        foreach(WaypointController waypoint in path)
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

        ReachedEndOfPath();
    }
}
