using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticExplosiveBulletController : ExplosiveBulletController
{
    private Vector3 horizontalSpeed; 
    private Vector3 verticalSpeed;
    [SerializeField] private float gravityForce;
    public override void Initialize(float speed, float lifeTime, int damage, LayerMask interactionLayerMask, LayerMask damageLayerMask)
    {
        base.Initialize(speed, lifeTime, damage, interactionLayerMask, damageLayerMask);
        Vector3 speedVector = transform.forward * speed;
        horizontalSpeed = Vector3.ProjectOnPlane(speedVector, Vector3.up);
        verticalSpeed = Vector3.Project(speedVector, Vector3.up);
    }

    protected override void Move()
    {
        transform.position += (horizontalSpeed + verticalSpeed) * Time.deltaTime;
        verticalSpeed += Vector3.down * (gravityForce * Time.deltaTime);
        transform.forward = (horizontalSpeed + verticalSpeed);
    }
}
