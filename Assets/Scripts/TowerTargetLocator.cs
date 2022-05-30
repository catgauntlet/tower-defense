using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetLocator : MonoBehaviour
{
    [SerializeField] float towerRange = 25f;
    [SerializeField] ParticleSystem projectileParticles;

    [SerializeField] Transform weapon;
    
    private Transform target;


    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        if (target)
        {
            float distanceToCurrentTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToCurrentTarget < towerRange)
            {
                return;
            }
        }
 
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        Transform closestTarget = null;
        float maxDistanceToSearch = towerRange;

        foreach (EnemyController enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistanceToSearch)
            {
                closestTarget = enemy.transform;
                maxDistanceToSearch = targetDistance;
            }
        }

        target = closestTarget;
    }

    private void AimWeapon()
    {
        if (target) {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            weapon.LookAt(target);
            bool targetInRange = targetDistance < towerRange;
            Attack(targetInRange);
        } else
        {
            Attack(false);
        }
    }

    private void Attack(bool isActive)
    {
        var emission = projectileParticles.emission;
        emission.enabled = isActive;
    }
}
