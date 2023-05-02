using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    private Transform _currentTarget;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform aimPoint;
     Quaternion _rot;
    private void Awake()
    {
        EventManager.AddListener<PlayerTargetChangeEvent>(OnPlayerTargetChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerTargetChangeEvent>(OnPlayerTargetChange);
    }

    private void OnPlayerTargetChange(PlayerTargetChangeEvent obj)
    {
        _currentTarget = obj.Target;
    }
    private void Update()
    {
            /*transform.rotation*/ _rot = Quaternion.RotateTowards(_rot,
                _currentTarget
                    ? Quaternion.LookRotation(-transform.position + _currentTarget.position)
                    : Quaternion.identity, rotationSpeed * Time.deltaTime);

            if (_currentTarget)
            {
                aimPoint.position = _currentTarget.position;
            }
            else
            {
                aimPoint.position = Vector3.forward * 3 + Vector3.up;
            }
    }
}