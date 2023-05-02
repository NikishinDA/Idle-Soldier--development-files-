using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpEffectController : BossEffectController
{
    [Header("Ragdoll")]
    [SerializeField] private Rigidbody[] ragdollBodies;
    [SerializeField] private Animator dollAnimator;
    public override void DeathEffect()
    {
        base.DeathEffect();
        dollAnimator.enabled = false;
        dollAnimator.transform.SetParent(null);
        foreach (var rigidbody in ragdollBodies)
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.AddExplosionForce(10f, explosionPoint.position, 5f, 1f, ForceMode.Impulse);
        }
    }
}
