using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int MaxHitPoints = 5;
    [Tooltip("Adds amount to max hitpoints every round")]
    [SerializeField] int DifficultyRamp = 1;
    [SerializeField] GameObject Explosion;
    private int currentHitpoints;

    public int CurrentHitpoints => currentHitpoints;
    Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable() {
        currentHitpoints = MaxHitPoints + Random.Range(-MaxHitPoints / 2 , MaxHitPoints / 2);
    }

    private void OnParticleCollision(GameObject other) 
    {
        currentHitpoints --;
        if(currentHitpoints <=0)
        {
            gameObject.SetActive(false);

            var explosionPosition = new Vector3(transform.position.x, 8, transform.position.z);
            var explosion = Instantiate(Explosion, explosionPosition, Quaternion.identity);
            Object.Destroy(explosion, 1.0f);
            GetComponent<EnemyMover>().ReturnToStart();
            enemy.RewardGold();
            MaxHitPoints += DifficultyRamp;
        }
    }

}
