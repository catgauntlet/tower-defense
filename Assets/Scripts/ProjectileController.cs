using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    public int GetDamage()
    {
        return damage;
    }
}
