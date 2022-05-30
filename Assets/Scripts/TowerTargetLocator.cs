using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<EnemyMovementController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        weapon.LookAt(target);
    }
}
