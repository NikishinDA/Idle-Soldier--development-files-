using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private float moneyBoostModifier;
    private int _playerDamage;
    private int _moneyTotal;
    private int _damageUpgradeLevel;
    private int _healthUpgradeLevel;
    private int _moneyUpgradeLevel;
    private int _damageUpgradeCost;
    private int _healthUpgradeCost;
    private int _moneyUpgradeCost;
    public int DamageUpgradeCost => _damageUpgradeCost;

    public int HealthUpgradeCost => _healthUpgradeCost;

    public int MoneyUpgradeCost => _moneyUpgradeCost;

    
    public int DamageUpgradeLevel => _damageUpgradeLevel;

    public int HealthUpgradeLevel => _healthUpgradeLevel;

    public int MoneyUpgradeLevel => _moneyUpgradeLevel;
    private float _moneyModifier = 1f;

    public Action<bool> WeaponEnoughMoney;
    public Action<bool> ArmorEnoughMoney;
    public Action<bool> MoneyEnoughMoney;
    private void Awake()
    {
        EventManager.AddListener<PlayerDamageChangeEvent>(OnPlayerDamageChange);
        EventManager.AddListener<MoneyCollectEvent>(OnPlayerMoneyCollect);
        EventManager.AddListener<EnemyKilledEvent>(OnEnemyKilled);
        EventManager.AddListener<PlayerCheckpointCrossEvent>(OnCheckpointCrossed);
        EventManager.RemoveListener<BoosterMoneyCollectEvent>(OnMoneyBoosterCollect);
        _moneyTotal = PlayerPrefs.GetInt(PlayerPrefsStrings.MoneyTotal, 0);
        _damageUpgradeLevel = PlayerPrefs.GetInt(PlayerPrefsStrings.WeaponUpgradeLevel, 0);
        _healthUpgradeLevel = PlayerPrefs.GetInt(PlayerPrefsStrings.MaxHealth, 1) - 1;
        _moneyUpgradeLevel = PlayerPrefs.GetInt(PlayerPrefsStrings.MoneyUpgradeLevel, 0);
        RecalculateDamageCost();
        RecalculateHealthCost();
        RecalculateMoneyCost();
    }


    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerDamageChangeEvent>(OnPlayerDamageChange);
        EventManager.RemoveListener<MoneyCollectEvent>(OnPlayerMoneyCollect);
        EventManager.RemoveListener<EnemyKilledEvent>(OnEnemyKilled);
        EventManager.RemoveListener<BoosterMoneyCollectEvent>(OnMoneyBoosterCollect);
        EventManager.RemoveListener<PlayerCheckpointCrossEvent>(OnCheckpointCrossed);
    }

    private void OnMoneyBoosterCollect(BoosterMoneyCollectEvent obj)
    {
        if (obj.Toggle)
        {
            _moneyModifier = moneyBoostModifier;
        }
        else
        {
            _moneyModifier = 1f;
        }
    }

    private void OnEnemyKilled(EnemyKilledEvent obj)
    {
        ChangeMoneyAmount(obj.Cost);
    }

    private void OnCheckpointCrossed(PlayerCheckpointCrossEvent obj)
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.MaxHealth, _healthUpgradeLevel + 1);
        PlayerPrefs.SetInt(PlayerPrefsStrings.WeaponUpgradeLevel, _damageUpgradeLevel);
        PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyUpgradeLevel, _moneyUpgradeLevel);
        PlayerPrefs.SetInt(PlayerPrefsStrings.MoneyTotal, _moneyTotal);
    }

    private void Start()
    {
        BroadcastAmountChangeEvent();
    }

    private void OnPlayerMoneyCollect(MoneyCollectEvent obj)
    {
        ChangeMoneyAmount(2 * _playerDamage);
        
    }

    private void OnPlayerDamageChange(PlayerDamageChangeEvent obj)
    {
        _playerDamage = obj.Damage;
    }

    private void ChangeMoneyAmount(int diff)
    {
        if (diff > 0)
        {
            diff = (int) (diff * (1 + 0.1f * _moneyUpgradeLevel) * _moneyModifier);
        }
        _moneyTotal += diff;
        BroadcastAmountChangeEvent();

        WeaponEnoughMoney.Invoke(_moneyTotal >= _damageUpgradeCost);
        ArmorEnoughMoney.Invoke(_moneyTotal >= _healthUpgradeCost);
        MoneyEnoughMoney.Invoke(_moneyTotal >= _moneyUpgradeCost);
    }

    private void BroadcastAmountChangeEvent()
    {
        var evt = GameEventsHandler.MoneyAmountChangeEvent;
        evt.Amount = _moneyTotal;
        EventManager.Broadcast(evt);
    }

    public bool TryWeaponUpgrade()
    {
        if (_moneyTotal >= _damageUpgradeCost)
        {
            ChangeMoneyAmount(-_damageUpgradeCost);
            _damageUpgradeLevel++;
            RecalculateDamageCost();
            BroadcastDpsUpgradeEvent();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool TryHealthUpgrade()
    {
        if (_moneyTotal >= _healthUpgradeCost)
        {
            ChangeMoneyAmount(-_healthUpgradeCost);
            _healthUpgradeLevel++;
            RecalculateHealthCost();
            BroadcastHealthUpgradeEvent();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool TryMoneyUpgrade()
    {
        if (_moneyTotal >= _moneyUpgradeCost)
        {
            ChangeMoneyAmount(-_moneyUpgradeCost);
            _moneyUpgradeLevel++;
            RecalculateMoneyCost();
            return true;
        }
        else
        {
            return false;
        }
    }
    private void RecalculateDamageCost()
    {
        _damageUpgradeCost = (int) (10 * Mathf.Pow(1.5f, _damageUpgradeLevel)) + 15;


        WeaponEnoughMoney.Invoke(_moneyTotal >= _damageUpgradeCost);
        ArmorEnoughMoney.Invoke(_moneyTotal >= _healthUpgradeCost);
        MoneyEnoughMoney.Invoke(_moneyTotal >= _moneyUpgradeCost);
    }
    private void RecalculateMoneyCost()
    {
        _moneyUpgradeCost = (int) (10 * Mathf.Pow(1.5f, _moneyUpgradeLevel)) + 15;


        WeaponEnoughMoney.Invoke(_moneyTotal >= _damageUpgradeCost);
        ArmorEnoughMoney.Invoke(_moneyTotal >= _healthUpgradeCost);
        MoneyEnoughMoney.Invoke(_moneyTotal >= _moneyUpgradeCost);
    }

    private void RecalculateHealthCost()
    {
        _healthUpgradeCost = (int) (10 * Mathf.Pow(1.5f, _healthUpgradeLevel)) + 15;


        WeaponEnoughMoney.Invoke(_moneyTotal >= _damageUpgradeCost);
        ArmorEnoughMoney.Invoke(_moneyTotal >= _healthUpgradeCost);
        MoneyEnoughMoney.Invoke(_moneyTotal >= _moneyUpgradeCost);
    }

    private void BroadcastDpsUpgradeEvent()
    {
        var evt = GameEventsHandler.PlayerDPSUpgradeEvent;
        evt.Level = _damageUpgradeLevel;
        EventManager.Broadcast(evt);
    }
    private void BroadcastHealthUpgradeEvent()
    {
        var evt = GameEventsHandler.PlayerArmorUpgradeEvent;
        evt.Level = _healthUpgradeLevel;
        EventManager.Broadcast(evt);
    }
}
