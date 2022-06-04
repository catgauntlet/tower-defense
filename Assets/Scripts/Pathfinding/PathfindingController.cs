using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int destinationCoordinates;

    public Vector2Int StartCoordinates
    {
        get { return startCoordinates; }
    }

    public Vector2Int DestinationCoordinates
    {
        get { return destinationCoordinates; }
    }

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
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(startCoordinates);
        return findPath();
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return findPath();
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
                neighbour.connectedTo = currentSearchNode;
                exploredNodes.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    private void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        exploredNodes.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        exploredNodes.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning == true)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private List<Node> findPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;
        currentNode.isPath = true;

        path.Add(currentNode);

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            currentNode.isPath = true;
            path.Add(currentNode);
        }

        path.Reverse();

        return path;
    } 

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousWalkableState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousWalkableState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyPathReceivers()
    {
        BroadcastMessage("RecalculatePath", false,SendMessageOptions.DontRequireReceiver);
    }
}
