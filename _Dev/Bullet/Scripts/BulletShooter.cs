using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [Header("Bullet Settings")] [SerializeField]
    private BulletController bulletPrefab;

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private LayerMask damageLayerMask;
    [SerializeField] private LayerMask interactionLayerMask;

    [Header("Shooting Settings")] [SerializeField]
    private float maxShootAngle;

    [SerializeField] private Transform muzzleTransform;

    private float _cd;
    private Transform _currentTarget;

    private bool _isActive = true;

    [SerializeField] private WeaponUpgradeManager _upgradeManager;

    private void Awake()
    {
        EventManager.AddListener<PlayerTargetChangeEvent>(OnPlayerTargetChange);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
       // _upgradeManager = GetComponent<WeaponUpgradeManager>();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerTargetChangeEvent>(OnPlayerTargetChange);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
    }

    private void Start()
    {
        _cd = _upgradeManager.CurrentSettings.Cooldown;
    }

    private void OnGameOver(GameOverEvent obj)
    {
        _isActive = false;
    }

    private void OnPlayerTargetChange(PlayerTargetChangeEvent obj)
    {
        _currentTarget = obj.Target;
    }

    private void Update()
    {
        if (_isActive && _currentTarget &&
            Vector3.Angle(transform.forward, _currentTarget.position - transform.position) <
            maxShootAngle)
        {
            if (_cd > 0)
            {
                _cd -= Time.deltaTime;
            }
            else
            {
                if (_upgradeManager.CurrentSettings.IsBurst)
                {
                    StartCoroutine(BurstCor(_upgradeManager.CurrentSettings.BulletsPerBurst,
                        _upgradeManager.CurrentSettings.BurstCooldown));
                    _cd = _upgradeManager.CurrentSettings.Cooldown;
                }
                else
                {
                    ShootBullet();
                    _cd = _upgradeManager.CurrentSettings.Cooldown;
                }
            }
        }
    }

    private IEnumerator BurstCor(int bulletNum, float burstCooldown)
    {
        for (int i = 0; i < bulletNum; i++)
        {
            ShootBullet();
            for (float t = 0; t < burstCooldown; t += Time.deltaTime)
            {
                yield return null;
            }
        }
    }

    public void SetMuzzleTransform(Transform muzzleTransform)
    {
        this.muzzleTransform = muzzleTransform;
    }
    public void ShootBullet()
    {
        Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.LookRotation(muzzleTransform.forward))
            .Initialize(bulletSpeed, bulletLifeTime, _upgradeManager.CurrentSettings.Damage, interactionLayerMask,
                damageLayerMask);
    }
}