using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{

    [SerializeField] [Range(0f, 5f)] private float movementSpeed = 1.0f;

    private List<Node> path = new List<Node>();
    private EnemyController enemy;
    private GridManager gridManager;
    private PathfindingController pathfinder;

    private void Awake()
    {
        enemy = GetComponent<EnemyController>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathfindingController>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(MoveToWaypoint());
    }
    
    void RecalculatePath()
    {
        path.Clear();
        path = pathfinder.GetNewPath();
        print(path.Count);
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void ReachedEndOfPath()
    {
        enemy.EnemyGoldStealHandler();
        gameObject.SetActive(false);
    }

    IEnumerator MoveToWaypoint()
    {
        for(int i = 0;  i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            print("Move from position: " + startPosition);
            print("Move to position: " + endPosition);
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
