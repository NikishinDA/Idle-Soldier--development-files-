using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectController : MonoBehaviour
{
    [SerializeField] protected Rigidbody[] rigidbodies;
    [SerializeField] private Animator animator;
   public virtual void DeathEffect()
    {
        if (animator)
            animator.enabled = false;
        foreach (var rigidbody in rigidbodies)
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }
        rigidbodies[0].AddForce((Random.insideUnitSphere + Vector3.forward + Vector3.up) * 100f, ForceMode.Impulse);
    }
}
