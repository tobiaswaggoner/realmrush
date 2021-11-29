using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    GameObject TowerPrefab;

    [SerializeField]
    int Costs = 50;

    public bool CreateTower(Vector3 position)
    {
        var bank = FindObjectOfType<Bank>();    
        if(bank==null) return false;
        if(bank.CurrentBalance<=Costs) return false;

        var tower = Instantiate(TowerPrefab);
        tower.transform.position = position;

        bank.Withdraw(Costs);

        return true;
    }
}