using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] Transform target;
    [SerializeField] float MaxDistance = 15f;
    [Range(0.1f, 10.0f)]
    [SerializeField] float ShootingRate = 1f;
    [Range(0.1f, 10.0f)]
    [SerializeField] float ShootingDistance = 1f;
    ParticleSystem ParticleSystem;
    ParticleSystem.EmissionModule EmissionModule;

    ParticleSystem.MainModule MainModule;

    private void Awake() 
    {
        ParticleSystem = weapon.GetComponentInChildren<ParticleSystem>();
        EmissionModule = ParticleSystem.emission;
        MainModule = ParticleSystem.main;
        MainModule.startLifetime = ShootingDistance;
        EmissionModule.rateOverTime = ShootingRate;
    }

    void Update()
    {
        MainModule.startLifetime = ShootingDistance;
        EmissionModule.rateOverTime = ShootingRate;

        FindClosestTarget();

        AimWeapon();
    }

    private void FindClosestTarget()
    {
        target = FindObjectsOfType<Enemy>()
            .Select( em => new {Enemy = em, Distance = Vector3.Distance(em.transform.position, transform.position)})
            .Where ( em => em.Distance<MaxDistance)
            .OrderByDescending(em => em.Enemy.AbsolutePosition)
            .FirstOrDefault()?
            .Enemy?.transform;
    }

    private void AimWeapon()
    {
        EmissionModule.enabled = target!=null;
        if(target==null) return;

//        weapon.LookAt(target);

        var neededRotation = Quaternion.LookRotation(target.position - weapon.parent.position);    
        weapon.rotation = Quaternion.RotateTowards(weapon.rotation, neededRotation, Time.deltaTime * 200f);

    }
}
