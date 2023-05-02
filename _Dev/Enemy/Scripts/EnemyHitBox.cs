using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour, IHitbox
{
    [SerializeField] private EnemyHealthManager healthManager;

    public void TakeDamage(int damage)
    {
        healthManager.TakeDamage(damage);
    }
}
