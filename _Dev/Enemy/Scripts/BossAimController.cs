using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAimController : MonoBehaviour
{
    [SerializeField] private Transform towerTransform;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private float towerRotationSpeed;
    [SerializeField] private float gunRotationSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private float lookFix;

    private Vector3 _lookRotation;

    private Quaternion _gunRotation;

    private void Awake()
    {
        lookFix = Mathf.Sign(lookFix);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        _lookRotation = Quaternion.LookRotation((towerTransform.position - target.position)*lookFix).eulerAngles;
        towerTransform.rotation = Quaternion.RotateTowards(towerTransform.rotation,
            Quaternion.Euler(0, _lookRotation.y, 0), towerRotationSpeed * Time.deltaTime);
        _gunRotation = gunTransform.rotation;
        _gunRotation = Quaternion.RotateTowards(_gunRotation,
            Quaternion.Euler(_lookRotation.x, _gunRotation.eulerAngles.y, _gunRotation.eulerAngles.z),
            gunRotationSpeed * Time.deltaTime);
        gunTransform.rotation = _gunRotation;
    }
}