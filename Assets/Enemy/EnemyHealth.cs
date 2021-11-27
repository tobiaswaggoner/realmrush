using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int MaxHitPoints = 5;
    private int CurrentHitpoints;

    private void OnEnable() {
        CurrentHitpoints = MaxHitPoints;
    }

    private void OnParticleCollision(GameObject other) 
    {
        CurrentHitpoints --;
        if(CurrentHitpoints <=0){
            gameObject.SetActive(false);
            GetComponent<EnemyMover>().ReturnToStart();
        }
    }

}
