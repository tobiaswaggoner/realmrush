using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI GoldText;

    [SerializeField]
    TextMeshProUGUI LivesText;

    [SerializeField]
    Bank bank;

    // Update is called once per frame
    void Update()
    {
        GoldText.text = $"{bank.CurrentBalance} Gold";
        LivesText.text = $"{bank.CurrentLives} Lives";
    }
}
