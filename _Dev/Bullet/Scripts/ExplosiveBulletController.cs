using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletController : BulletController
{
    [Header("Effect")]
    [SerializeField] private GameObject explosionEffect;

    [Header("Explosion")] [SerializeField] private float explosionRadius = 0.2f;

    private Collider[] _explosionColliders;
    protected override void CheckHit(RaycastHit hit)
    {
        Instantiate(explosionEffect, hit.point, Quaternion.identity).transform.SetParent(hit.transform);
        _explosionColliders = Physics.OverlapSphere(hit.point, explosionRadius, damageLayerMask);
        foreach (var hitCollider in _explosionColliders)
        {
            hitCollider.GetComponent<IHitbox>().TakeDamage(damage);
        }
    }
}
