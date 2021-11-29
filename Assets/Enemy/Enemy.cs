using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyHealth))]

public class Enemy : MonoBehaviour
{
    [SerializeField] int GoldReward = 25;
    [SerializeField] int LivePenalty = 1;
    Bank Bank;
    [SerializeField] TextMeshPro label;


    public float AbsolutePosition => mover.AbsolutePosition;

    EnemyMover mover;
    EnemyHealth health;

    // Start is called before the first frame update
    void Start()
    {
        Bank = FindObjectOfType<Bank>();
        mover = GetComponent<EnemyMover>();
        health = GetComponent<EnemyHealth>();
    }

    private void Update() 
    {
        label.text = "RAM\r\nPos:" + AbsolutePosition.ToString("0.0")+ "\r\n" + health.CurrentHitpoints;    
    }

    public void RewardGold()
    {
        if(Bank==null) { return; }
        Bank.Deposit(GoldReward);
    }
    public void ReduceLives()
    {
        if(Bank==null) { return; }
        Bank.DecreaseLive(LivePenalty);
    }   

}
