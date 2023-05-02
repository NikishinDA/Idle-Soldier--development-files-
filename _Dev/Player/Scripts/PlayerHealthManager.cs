using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int threshold;
    private bool _isActive = true;
    private int Health
    {
        get => health;
        set
        {
            health = value;
            healthIndicator.UpdateHealth(value);
            armorManager.HealthUpdate(value);
        }
    }

    [SerializeField] private HealthIndicatorController healthIndicator; 
    [SerializeField] private PlayerArmorManager armorManager;

    private void Awake()
    {
        EventManager.AddListener<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
        EventManager.AddListener<MagnetActivationEvent>(OnMagnetActivation);
        EventManager.AddListener<ArmorCollectEvent>(OnArmorCollect);
        EventManager.AddListener<PlayerArmorUpgradeEvent>(OnPlayerArmorUpgrade);
        EventManager.AddListener<PlayerCheckpointCrossEvent>(OnCheckPointCross);
        Health = PlayerPrefs.GetInt(PlayerPrefsStrings.CurrentHealth, 1);
        threshold = PlayerPrefs.GetInt(PlayerPrefsStrings.MaxHealth, 1);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
        EventManager.RemoveListener<MagnetActivationEvent>(OnMagnetActivation);
        EventManager.RemoveListener<ArmorCollectEvent>(OnArmorCollect);
        EventManager.RemoveListener<PlayerArmorUpgradeEvent>(OnPlayerArmorUpgrade);
        EventManager.RemoveListener<PlayerCheckpointCrossEvent>(OnCheckPointCross);

    }


    private void OnCheckPointCross(PlayerCheckpointCrossEvent obj)
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.CurrentHealth, health);
        //PlayerPrefs.SetInt(PlayerPrefsStrings.MaxHealth, threshold);
    }

    private void OnPlayerArmorUpgrade(PlayerArmorUpgradeEvent obj)
    {
        threshold = obj.Level + 1;
        Health = threshold;
        BroadcastMaxArmorChangeEvent();
    }

    private void BroadcastMaxArmorChangeEvent()
    {
        var evt = GameEventsHandler.PlayerMaxArmorChangeEvent;
        evt.MaxArmor = threshold;
        EventManager.Broadcast(evt);
    }

    private void OnArmorCollect(ArmorCollectEvent obj)
    {
        if (Health < threshold)
            Health++;
        else
            EventManager.Broadcast(GameEventsHandler.MoneyCollectEvent);
        
    }

    private void OnMagnetActivation(MagnetActivationEvent obj)
    {
        Health = 1;
    }

    private void Start()
    {
        healthIndicator.UpdateHealth(health);
        BroadcastMaxArmorChangeEvent();
    }

    private void OnPlayerTakeDamage(PlayerTakeDamageEvent obj)
    {
        if (_isActive)
        {
            Health -= obj.Damage;
            if (health <= 0)
            {
                healthIndicator.gameObject.SetActive(false);
                EventManager.Broadcast(GameEventsHandler.GameOverEvent);
                _isActive = false;
            }
        }
    }
}
