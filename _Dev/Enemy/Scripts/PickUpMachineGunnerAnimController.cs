using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMachineGunnerAnimController : MonoBehaviour
{
    [SerializeField] private Transform leftHandHolder;
    [SerializeField] private Transform rightHandHolder;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandHolder.position);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandHolder.position);
    }
}
