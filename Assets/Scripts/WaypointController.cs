using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] GameObject towerObject;
    [SerializeField] bool isBuildableTile;

    private GameObject towerSpawnParent;

    public bool IsBuildableTile
    { get { return isBuildableTile; } }

    private void Start()
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
        isBuildableTile = false;
        GameObject spawnedTower = Instantiate(towerObject, transform.position, Quaternion.identity);
        spawnedTower.transform.parent = towerSpawnParent.transform;
    }
}
