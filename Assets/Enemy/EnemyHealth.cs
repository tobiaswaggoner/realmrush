using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int MaxHitPoints = 5;
    [Tooltip("Adds amount to max hitpoints every round")]
    [SerializeField] int DifficultyRamp = 1;
    private int currentHitpoints;

    public int CurrentHitpoints => currentHitpoints;
    Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable() {
        currentHitpoints = MaxHitPoints;
    }

    private void OnParticleCollision(GameObject other) 
    {
        currentHitpoints --;
        if(currentHitpoints <=0)
        {
            gameObject.SetActive(false);
            GetComponent<EnemyMover>().ReturnToStart();
            enemy.RewardGold();
            MaxHitPoints += DifficultyRamp;
        }
    }

}
