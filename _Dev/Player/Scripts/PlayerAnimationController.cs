using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator femAnimator;
    [SerializeField] private Animator malAnimator;
    private int PistolRun = Animator.StringToHash("Pistol Run");
    private int RifleRun = Animator.StringToHash("Rifle Run");
    private int MGRun = Animator.StringToHash("MG Run");
    private Animator _animator;
    private WeaponType _weaponType;
    private void Awake()
    {
        EventManager.AddListener<PlayerWeaponChangeEvent>(OnWeaponChanged);
        EventManager.AddListener<PlayerModelChangeEvent>(OnModelChanged);

        _animator = PlayerPrefs.GetInt(PlayerPrefsStrings.Gen, 1) == 1 ? _animator = malAnimator : _animator = femAnimator;
    }

    private void OnModelChanged(PlayerModelChangeEvent obj)
    {
        if (obj.Bin)
        {
            _animator = malAnimator;
        }
        else
        {
            _animator = femAnimator;
        }
        ChangeAnimationType(_weaponType);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerWeaponChangeEvent>(OnWeaponChanged);

        EventManager.RemoveListener<PlayerModelChangeEvent>(OnModelChanged);
    }

    private void OnWeaponChanged(PlayerWeaponChangeEvent obj)
    {
        ChangeAnimationType(obj.WeaponType);
        _weaponType = obj.WeaponType;
    }

    private void ChangeAnimationType(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.OneHanded:
                {
                    _animator.SetBool(PistolRun, true);
                    _animator.SetBool(RifleRun, false);
                    _animator.SetBool(MGRun, false);
                }
                break;
            case WeaponType.TwoHanded:
                {
                    _animator.SetBool(PistolRun, false);
                    _animator.SetBool(RifleRun, true);
                    _animator.SetBool(MGRun, false);
                }
                break;
            case WeaponType.MachineGun:
                {
                    _animator.SetBool(PistolRun, false);
                    _animator.SetBool(RifleRun, false);
                    _animator.SetBool(MGRun, true);
                }
                break;
        }
    }
}
