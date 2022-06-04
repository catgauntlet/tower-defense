using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] int towerBuildCost = 75;
    [SerializeField] float towerBuildTime = 1f;

    private void Start()
    {
        StartCoroutine(BuildTower());
    }

    private IEnumerator BuildTower()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child)
            {
                child.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(towerBuildTime);

            foreach (Transform grandchild in child)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public TowerController CreateTower (Vector3 position)
    {
        BankController bank = FindObjectOfType<BankController>();
        if (bank.CurrentBalance >= towerBuildCost)
        {
            TowerController spawnedTower = Instantiate(this, position, Quaternion.identity);
            bank.Withdraw(towerBuildCost);
            return spawnedTower;
        }

        return null;
    }
}
