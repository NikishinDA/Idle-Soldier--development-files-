using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelController : MonoBehaviour
{
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private WeaponType type;
    public Transform GetMuzzleTransform()
    {
        return muzzleTransform;
    }
    public Transform GetLeftHandTarget()
    {
        return leftHandTarget;
    }
    public WeaponType GetWeaponType()
    {
        return type;
    }
}
