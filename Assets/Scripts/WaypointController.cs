using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] GameObject towerObject;
    [SerializeField] bool isBuildableTile;

    private GameObject towerSpawnParent;
    private bool hasTowerBuilt = false;

    private void Start()
    {
        towerSpawnParent = GameObject.FindWithTag("TowerSpawn");
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isBuildableTile)
        {
            if (!hasTowerBuilt)
            {
                BuildTower();
            }
        }
    }

    private void BuildTower()
    {
        hasTowerBuilt = true;
        GameObject spawnedTower = Instantiate(towerObject, transform.position, Quaternion.identity);
        spawnedTower.transform.parent = towerSpawnParent.transform;
    }
}
