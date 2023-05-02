using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstEnemyBulletShooter : EnemyBulletShooter
{
    [SerializeField] private float burstInterval;
    [SerializeField] private int bulletsPerBurst;
    protected override void ShootBullet()
    {
        StartCoroutine(BurstCor(bulletsPerBurst, burstInterval));
    }
    private IEnumerator BurstCor(int bulletNum, float burstCooldown)
    {
        for (int i = 0; i < bulletNum; i++)
        {
            base.ShootBullet();
            for (float t = 0; t < burstCooldown; t += Time.deltaTime)
            {
                yield return null;
            }
        }
    }
}
