using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticEnemyBulletShooter : EnemyBulletShooter
{
    [SerializeField] private float distance;
    protected override void ShootBullet()
    {
        BallisticExplosiveBulletController go = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.LookRotation(muzzleTransform.forward)) as BallisticExplosiveBulletController;
        go.Initialize(bulletSpeed, bulletLifeTime, bulletDamage, interactionLayerMask, damageLayerMask);
    }
}
