using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _playerDamage;
    private int _playerMaxArmor;
    private float _playerSpeed;
    private void Awake()
    {
        EventManager.AddListener<PlayerDamageChangeEvent>(OnPlayerDamageChange);
        EventManager.AddListener<PlayerMaxArmorChangeEvent>(OnPlayerMaxArmorChange);
        EventManager.AddListener<EnemySpawnedEvent>(OnEnemySpawned);
        EventManager.AddListener<LevelSpeedChangeEvent>(OnSpeedChange);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<EnemySpawnedEvent>(OnEnemySpawned);
        EventManager.RemoveListener<PlayerDamageChangeEvent>(OnPlayerDamageChange);
        EventManager.RemoveListener<PlayerMaxArmorChangeEvent>(OnPlayerMaxArmorChange);
        EventManager.RemoveListener<LevelSpeedChangeEvent>(OnSpeedChange);
    }

    private void OnSpeedChange(LevelSpeedChangeEvent obj)
    {
        _playerSpeed = obj.Speed;
    }

    private void OnPlayerMaxArmorChange(PlayerMaxArmorChangeEvent obj)
    {
        _playerMaxArmor = obj.MaxArmor;
    }

    private void OnPlayerDamageChange(PlayerDamageChangeEvent obj)
    {
        _playerDamage = obj.Damage;
    }

    private void OnEnemySpawned(EnemySpawnedEvent obj)
    {
        obj.EnemyController.Initialize(_playerDamage, _playerMaxArmor, _playerSpeed);
    }
}
