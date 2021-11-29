using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int startingLives = 150;
    
    int currentBalance;
    int currentLives;

    public int CurrentBalance => currentBalance;
    public int CurrentLives => currentLives;

    void Start()
    {
        currentBalance = startingBalance;
        currentLives = startingLives;
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
    }
    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        currentBalance = Mathf.Max(0, currentBalance);
    }

    public void DecreaseLive(int amount)
    {
        currentLives -= Mathf.Abs(amount);
        currentLives = Mathf.Max(0, currentLives);

        if(currentLives <=0 )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
