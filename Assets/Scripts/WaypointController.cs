using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField] bool isBuildableTile;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isBuildableTile)
        {
            print("Build a tower at " + transform.name);
        }
    }
}
