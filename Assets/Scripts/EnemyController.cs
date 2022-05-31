using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    BankController bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<BankController>();
    }

    public int EnemyDeathHandler()
    {
        if (bank == null)
        {
            return 0;
        }

        bank.Deposit(goldReward);
        return 1;
    }

    public int EnemyGoldStealHandler()
    {
        if (bank == null)
        {
            return 0;
        }

        bank.Withdraw(goldPenalty);
        return 1;
    }
}
