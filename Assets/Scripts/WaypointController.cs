using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] TowerController towerObject;
    [SerializeField] bool isBuildableTile;

    public bool IsBuildableTile
    { get { return isBuildableTile; } }

    private GameObject towerSpawnParent;

    void Start()
    {
        towerSpawnParent = GameObject.FindWithTag("TowerSpawn");
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isBuildableTile)
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
        }
    }
}
