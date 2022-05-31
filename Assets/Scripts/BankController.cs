using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankController : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    [SerializeField]
    private int currentBalance;

    GameManager gameManager;

    UIController ui;

    public int CurrentBalance
    {
        get { return currentBalance;  }
    }

    // Start is called before the first frame update
    void Awake()
    {
        ui = FindObjectOfType<UIController>();
        gameManager = FindObjectOfType<GameManager>();
        currentBalance = startingBalance;
        ui.SetCurrencyAmount(currentBalance);
    }

    private void UpdateCurrencyLabel()
    {
        ui.SetCurrencyAmount(currentBalance);
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateCurrencyLabel();
    }

    public void Withdraw(int amount)
    {
        int absAmount = Mathf.Abs(amount);
        currentBalance -= absAmount;
        UpdateCurrencyLabel();

        if (currentBalance < 0)
        {
            gameManager.GameOver();
        }
    }
}
