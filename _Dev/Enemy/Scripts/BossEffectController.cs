using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffectController : EnemyEffectController
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] protected Transform explosionPoint;

    public override void DeathEffect() 
    {
        RaycastHit hit;
        if (Physics.Raycast(explosionPoint.position, Vector3.down, out hit, 10f, layerMask, QueryTriggerInteraction.Ignore))
        {
            Instantiate(explosionEffect, hit.point, Quaternion.identity).transform.SetParent(hit.transform);
        }
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.transform.SetParent(null);
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.AddExplosionForce(10f, explosionPoint.position, 5f, 1f, ForceMode.Impulse);
            rigidbody.AddTorque(Random.insideUnitSphere * 100f, ForceMode.Impulse);
        }
    }
}
