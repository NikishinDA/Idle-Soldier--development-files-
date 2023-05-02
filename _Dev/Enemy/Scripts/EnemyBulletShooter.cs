using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBulletShooter : MonoBehaviour
{ 
    [Header("Bullet Settings")]
    [SerializeField]
    protected BulletController bulletPrefab;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float bulletLifeTime;
    [SerializeField] protected int bulletDamage;
    [SerializeField] protected LayerMask damageLayerMask;
    [SerializeField] protected LayerMask interactionLayerMask;
    [SerializeField] private float damageMultiplier = 1f;
    [Header("Shooter Settings")] [SerializeField]
    private float cooldown;

    [SerializeField] private bool randomShootingStartTime;

    [SerializeField] protected Transform muzzleTransform;
    private bool _isActive;

    private float _cd;

    private void Awake()
    {
        if (randomShootingStartTime)
        {
            _cd = Random.Range(0, cooldown);
        }
        else
        {
            _cd = cooldown;
        }
    }

    void Update()
    {
        if (_isActive)
        {
            if (_cd > 0)
            {
                _cd -= Time.deltaTime;
            }
            else
            {
                ShootBullet();
                _cd = cooldown;
            }
        }
    }
    public void SetActive(bool isActive)
    {
        _isActive = isActive;
    }
    protected virtual void ShootBullet()
    {
        Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.LookRotation(muzzleTransform.forward))
            .Initialize(bulletSpeed, bulletLifeTime, bulletDamage, interactionLayerMask, damageLayerMask);
    }


    public void Initialize(int playerArmor)
    {
        bulletDamage = (int)((playerArmor / 5 + 1) * damageMultiplier);
    }

    public int GetDamage()
    {
        return bulletDamage;
    }
}
