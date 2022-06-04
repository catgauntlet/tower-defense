using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] TowerController towerObject;
    [SerializeField] bool isBuildableTile;

    public bool IsBuildableTile
    { get { return isBuildableTile; } }

    private GameObject towerSpawnParent;

    private GridManager gridManager;
    private PathfindingController pathFinder;

    private Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathfindingController>();
    }

    void Start()
    {
        towerSpawnParent = GameObject.FindWithTag("TowerSpawn");
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(this.transform.position);

            if (!IsBuildableTile)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            BuildTower();
        }
    }

    private void BuildTower()
    {
        TowerController spawnedTower = towerObject.CreateTower(transform.position);
        if (spawnedTower != null)
        {
            isBuildableTile = false;
            spawnedTower.transform.parent = towerSpawnParent.transform;
            gridManager.BlockNode(coordinates);
        }
    }
}
