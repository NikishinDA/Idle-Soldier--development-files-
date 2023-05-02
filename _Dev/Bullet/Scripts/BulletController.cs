using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] protected int damage;
    [SerializeField] private LayerMask interactionLayerMask;
    [SerializeField] protected LayerMask damageLayerMask;

    [SerializeField] private bool enableEffects;
    [SerializeField]
        private ParticleSystem maleHitEffect;
    [SerializeField] private ParticleSystem femHitEffect;

    private void Start()
    {
        StartCoroutine(LifeTime(lifeTime));
    }

    public virtual void Initialize(float speed, float lifeTime, int damage, LayerMask interactionLayerMask, LayerMask damageLayerMask)
    {
        this.speed = speed;
        this.lifeTime = lifeTime;
        this.damage = damage;
        this.interactionLayerMask = interactionLayerMask;
        this.damageLayerMask = damageLayerMask;
    }
    private void LateUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,
            transform.forward, out hit, speed * Time.deltaTime * 1.5f, interactionLayerMask, QueryTriggerInteraction.Ignore))
        {
            transform.Translate(Vector3.forward * hit.distance);
            CheckHit(hit);
            Destroy(gameObject);
        }
        else if (Physics.Raycast(transform.position,
            -transform.forward, out hit, speed * Time.deltaTime  * 1.5f, interactionLayerMask, QueryTriggerInteraction.Ignore))
        {
            transform.Translate(Vector3.forward * hit.distance);
            CheckHit(hit);
            Destroy(gameObject);
        }
        else
        {
            Move();
        }
    }

    protected virtual void CheckHit(RaycastHit hit)
    {
        if (damageLayerMask == (damageLayerMask | (1 << hit.collider.gameObject.layer)))
        {
            hit.collider.GetComponent<IHitbox>().TakeDamage(damage);
            if (enableEffects) {
                if (VarSaver.Bin)
                    Instantiate(maleHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                else
                    Instantiate(femHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private IEnumerator LifeTime(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
