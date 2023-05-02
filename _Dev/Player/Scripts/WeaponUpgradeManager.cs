using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSettings
{
    public readonly int Damage;
    public readonly float Cooldown;
    public readonly bool IsBurst;
    public readonly int BulletsPerBurst;
    public readonly float BurstCooldown;

    public ShootingSettings(int damage, float cooldown, int bulletsPerBurst, float burstCooldown)
    {
        Damage = damage;
        Cooldown = cooldown;
        BulletsPerBurst = bulletsPerBurst;
        BurstCooldown = burstCooldown;
        IsBurst = bulletsPerBurst > 1;
    }

    public ShootingSettings(int damage, float cooldown)
    {
        Damage = damage;
        Cooldown = cooldown;
        IsBurst = false;
    }

    public int GetDPS()
    {
        if (BulletsPerBurst > 1)
        {
            return Mathf.RoundToInt(Damage * BulletsPerBurst / Cooldown);
        }
        else
        {
            return Mathf.RoundToInt(Damage / Cooldown);
        }
    }
}

public class WeaponUpgradeManager : MonoBehaviour
{
    [SerializeField] private int levelBoostModifier;
    private readonly ShootingSettings[] _shootingSettings =
    {
        /*new ShootingSettings(1, 1f),//pistol
        new ShootingSettings(2, 1f),
        new ShootingSettings(1, 1f, 3, 0.1f),//uzi
        new ShootingSettings(1, 1f, 4, 0.1f),
        new ShootingSettings(1, 0.2f),//mp5
        new ShootingSettings(1, 0.167f),
        new ShootingSettings(2, 0.286f),//ar-15
        new ShootingSettings(2, 0.25f),
        new ShootingSettings(3, 0.333f),//m60
        new ShootingSettings(3, 0.3f),
        new ShootingSettings(2, 0.182f),//m134
        new ShootingSettings(2, 0.167f),
        new ShootingSettings(2, 0.154f),
        new ShootingSettings(2, 0.143f),
        new ShootingSettings(2, 0.133f),
        new ShootingSettings(2, 0.125f),*/
        new ShootingSettings(1, 1f),//pistol
        new ShootingSettings(2, 1f),
        new ShootingSettings(3, 1f),
        new ShootingSettings(4, 1f),
        new ShootingSettings(1, 0.2f),//uzi
        new ShootingSettings(1, 0.167f),
        new ShootingSettings(1, 0.143f),
        new ShootingSettings(1, 0.125f),
        new ShootingSettings(2, 0.222f),//mp5
        new ShootingSettings(2, 0.2f),
        new ShootingSettings(2, 0.166f),
        new ShootingSettings(2, 0.143f),
        new ShootingSettings(3, 0.187f),//ar-15
        new ShootingSettings(3, 0.167f),
        new ShootingSettings(3, 0.15f),
        new ShootingSettings(3, 0.136f),
        new ShootingSettings(4, 0.166f),//m60
        new ShootingSettings(4, 0.154f),
        new ShootingSettings(4, 0.143f),
        new ShootingSettings(4, 0.133f),
        new ShootingSettings(5, 0.147f),//m134
        new ShootingSettings(5, 0.132f),
        new ShootingSettings(5, 0.117f),
        new ShootingSettings(5, 0.1f)
    };

    private int _upgradeLevel = 0;
    private ShootingSettings _currentSettings;

    public ShootingSettings CurrentSettings => _currentSettings;
    private int _levelModifier;

    private void Awake()
    {
        EventManager.AddListener<PlayerDPSUpgradeEvent>(OnPlayerDPSUpgrade);
        EventManager.AddListener<BoosterWeaponCollectEvent>(OnWeaponBoostCollect);
        EventManager.AddListener<PlayerModelChangeEvent>(OnModelChangeEvent);
        //EventManager.AddListener<PlayerCheckpointCrossEvent>(OnCheckPointCross);
        _upgradeLevel = PlayerPrefs.GetInt(PlayerPrefsStrings.WeaponUpgradeLevel, 0);
        UpdateShootingSettings();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerDPSUpgradeEvent>(OnPlayerDPSUpgrade);
        EventManager.RemoveListener<BoosterWeaponCollectEvent>(OnWeaponBoostCollect);

        EventManager.RemoveListener<PlayerModelChangeEvent>(OnModelChangeEvent);
        //EventManager.RemoveListener<PlayerCheckpointCrossEvent>(OnCheckPointCross);
    }

    private void OnModelChangeEvent(PlayerModelChangeEvent obj)
    {
        BroadcastDamageChangeEvent();
    }

    private void OnWeaponBoostCollect(BoosterWeaponCollectEvent obj)
    {
        if (obj.Toggle)
        {
            _levelModifier = levelBoostModifier;
        }
        else
        {
            _levelModifier = 0;
        }
        
        UpdateShootingSettings();
        BroadcastDamageChangeEvent();
    }

    private void Start()
    {
        BroadcastDamageChangeEvent();
    }
    private void OnPlayerDPSUpgrade(PlayerDPSUpgradeEvent obj)
    {
        _upgradeLevel = obj.Level;
        UpdateShootingSettings();
        BroadcastDamageChangeEvent();
    }

    /* private void OnCheckPointCross(PlayerCheckpointCrossEvent obj)
     {
         PlayerPrefs.SetInt(PlayerPrefsStrings.WeaponUpgradeLevel, _upgradeLevel);
     }*/
    private void UpdateShootingSettings()
    {
        int level = _upgradeLevel + _levelModifier;
        if (level< _shootingSettings.Length)
        {
            _currentSettings = _shootingSettings[level];
        }
        else
        {
            _currentSettings = new ShootingSettings(5 + (level - _shootingSettings.Length + 1), 0.1f);
        }
    }

    private void BroadcastDamageChangeEvent()
    {
        var evt = GameEventsHandler.PlayerDamageChangeEvent;
        evt.Damage = _currentSettings.GetDPS();
        evt.UpgradeLevel = _upgradeLevel + _levelModifier;
        EventManager.Broadcast(evt);
    }
}