using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] int towerBuildCost = 75;

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
