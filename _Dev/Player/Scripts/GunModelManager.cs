using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WeaponType
{
    OneHanded,
    TwoHanded,
    MachineGun
}
public class GunModelManager : MonoBehaviour
{
    [SerializeField] private WeaponModelController[] guns;
    [SerializeField] private int[] upgradeLevels;
    private readonly Dictionary<int, WeaponModelController> _gunDictionary = new Dictionary<int, WeaponModelController>();
    private WeaponModelController _currentModel;
    private int _lastLevel = -1;
    private BulletShooter _bulletShooter;
    private List<int> _keys;
    [SerializeField] private Transform[] leftHandIKTransforms;
    private Transform _leftHandTarget;
    private void Awake()
    {
        _bulletShooter = GetComponent<BulletShooter>();
        for (int i = 0; i < guns.Length; i++)
        {
            _gunDictionary.Add(upgradeLevels[i], guns[i]);
        }
        EventManager.AddListener<PlayerDamageChangeEvent>(OnPlayerDamageChange);
        ChangeModel(PlayerPrefs.GetInt(PlayerPrefsStrings.WeaponUpgradeLevel, 0));
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerDamageChangeEvent>(OnPlayerDamageChange);
    }
    
    
    private void OnPlayerDamageChange(PlayerDamageChangeEvent obj)
    {
       ChangeModel(obj.UpgradeLevel);  
    }

    private void ChangeModel(int level)
    {
        if (level == _lastLevel) return;
        if (level > upgradeLevels[upgradeLevels.Length - 1])
        {
            level = upgradeLevels[upgradeLevels.Length - 1];
        }
        else if (!_gunDictionary.ContainsKey(level))
        {
            _keys = _gunDictionary.Keys.ToList();
            for (int i = 1; i < _keys.Count; i++)
            {
                if (level < _keys[i])
                {
                    level = _keys[i - 1];
                    break;
                }
            }
        }
        if (_lastLevel != -1) _gunDictionary[_lastLevel].gameObject.SetActive(false);
        _lastLevel = level;
        _currentModel = _gunDictionary[level];
        _currentModel.gameObject.SetActive(true);
        _bulletShooter.SetMuzzleTransform(_currentModel.GetMuzzleTransform());
        _leftHandTarget = _currentModel.GetLeftHandTarget();
        foreach (var transform in leftHandIKTransforms)
        {
            transform.position = _leftHandTarget.position;
        }
        var evt = GameEventsHandler.PlayerWeaponChangeEvent;
        evt.WeaponType = _currentModel.GetWeaponType();
        EventManager.Broadcast(evt);
    }
}
