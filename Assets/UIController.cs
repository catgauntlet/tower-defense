using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TMP_Text currencyLabel;

    public void SetCurrencyAmount(int amount)
    {
        currencyLabel.text = $"Gold: {amount.ToString()}";
    }
}
