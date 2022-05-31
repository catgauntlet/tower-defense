using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankController : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    [SerializeField]
    private int currentBalance;

    public int CurrentBalance
    {
        get { return currentBalance;  }
    }

    // Start is called before the first frame update
    void Awake()
    {
        currentBalance = startingBalance;
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }

    public void Withdraw(int amount)
    {
        int absAmount = Mathf.Abs(amount);
        if (currentBalance - absAmount < 0)
        {
            currentBalance = 0;
        } else
        {
            currentBalance -= absAmount;
        }
    }
}
