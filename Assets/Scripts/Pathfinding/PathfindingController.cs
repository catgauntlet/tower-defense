using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;

    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode;

    private Dictionary<Vector2Int, Node> exploredNodes = new Dictionary<Vector2Int, Node>();
    private Queue<Node> frontier = new Queue<Node>();
    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    private GridManager gridManager;
    private Dictionary<Vector2Int, Node> grid;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }

        startNode = new Node(startCoordinates, true);
        destinationNode = new Node(destinationCoordinates, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        BreadthFirstSearch();
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbours = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neightborCoordinates = new Vector2Int(currentSearchNode.coordinates.x + direction.x, currentSearchNode.coordinates.y + direction.y);
            if (grid.ContainsKey(neightborCoordinates))
            {
               neighbours.Add(grid[neightborCoordinates]);
            }
        }

        foreach(Node neighbour in neighbours)
        {
            if (!exploredNodes.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                exploredNodes.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    private void BreadthFirstSearch()
    {
        bool isRunning = true;

        frontier.Enqueue(startNode);
        exploredNodes.Add(startNode.coordinates, startNode);

        while(frontier.Count > 0 && isRunning == true)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                currentSearchNode.isPath = true;
                isRunning = false;
            }
        }
    }
}
