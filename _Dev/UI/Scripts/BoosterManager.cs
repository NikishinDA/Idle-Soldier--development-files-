using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterManager : MonoBehaviour
{
    [Header("View")] [SerializeField] private GameObject speedIndicator;
    [SerializeField] private GameObject weaponIndicator;
    [SerializeField] private GameObject moneyIndicator;
    [SerializeField] private Image speedFill;
    [SerializeField] private Image weaponFill;
    [SerializeField] private Image moneyFill;

    [Header("Positions")] [SerializeField] private Vector2Int[] positions;
    [Header("Time")] 
    [SerializeField] private float speedEffectDuration;
    [SerializeField] private float moneyEffectDuration;
    [SerializeField] private float weaponEffectDuration;
    private IEnumerator _speedBoostCor;
    private IEnumerator _moneyBoostCor;
    private IEnumerator _weaponBoostCor;
    private void Awake()
    {
        EventManager.AddListener<BoosterSpeedCollectEvent>(OnSpeedBoosterCollect);
        EventManager.AddListener<BoosterMoneyCollectEvent>(OnMoneyBoosterCollect);
        EventManager.AddListener<BoosterWeaponCollectEvent>(OnWeaponBoosterCollect);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<BoosterSpeedCollectEvent>(OnSpeedBoosterCollect);
        EventManager.RemoveListener<BoosterMoneyCollectEvent>(OnMoneyBoosterCollect);
        EventManager.RemoveListener<BoosterWeaponCollectEvent>(OnWeaponBoosterCollect);
    }

    private void OnSpeedBoosterCollect(BoosterSpeedCollectEvent obj)
    {
        if (obj.Toggle)
        {
            if (_speedBoostCor != null)
            {
                StopCoroutine(_speedBoostCor);
            }

            _speedBoostCor = BoosterEffectCor(speedEffectDuration, speedIndicator, speedFill, obj);
            StartCoroutine(
                _speedBoostCor);
        }
    }

    private void OnMoneyBoosterCollect(BoosterMoneyCollectEvent obj)
    {
        if (obj.Toggle)
        {
            if (_moneyBoostCor != null)
            {
                StopCoroutine(_moneyBoostCor);
            }
            _moneyBoostCor =BoosterEffectCor(moneyEffectDuration,moneyIndicator, moneyFill, obj);
            StartCoroutine(
                _moneyBoostCor);
        }
    }

    private void OnWeaponBoosterCollect(BoosterWeaponCollectEvent obj)
    {
        if (obj.Toggle)
        {
            if (_weaponBoostCor != null)
            {
                StopCoroutine(_weaponBoostCor);
            }

            _weaponBoostCor = BoosterEffectCor(weaponEffectDuration, weaponIndicator, weaponFill, obj);
            StartCoroutine(_weaponBoostCor);
        }
    }


    private IEnumerator BoosterEffectCor(float fullTime, GameObject indicatorObject, Image fill, BoosterEvent gameEvent)
    {
        indicatorObject.SetActive(true);
        for (float t = fullTime; t >= 0; t -= Time.deltaTime)
        {
            fill.fillAmount = t / fullTime;
            yield return null;
        }
        gameEvent.Toggle = false;
        EventManager.Broadcast(gameEvent);
        
        indicatorObject.SetActive(false);
    }
}