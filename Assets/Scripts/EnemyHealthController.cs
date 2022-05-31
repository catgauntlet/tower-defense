using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 100;

    [SerializeField] int currentHitPoints = 0;

    EnemyController enemy;

    private void Start()
    {
        enemy = GetComponent<EnemyController>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject particleObject)
    {
        print("Hit: " + particleObject.name);
        if (particleObject.tag == "TowerProjectile")
        {
            HandleProjectileHit(particleObject);
        }
    }

    private void HandleProjectileHit(GameObject particleObject)
    {
        ProjectileController projectile = particleObject.GetComponent<ProjectileController>();
        currentHitPoints -= projectile.GetDamage();

        if (currentHitPoints <= Mathf.Epsilon)
        {
            enemy.EnemyDeathHandler();
            gameObject.SetActive(false);
        }
    }
}
