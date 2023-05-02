using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem moneyCollectEffect;
    [SerializeField] private ParticleSystem armorCollectEffect;
    [SerializeField] private ParticleSystem boosterCollectEffect;

    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private Animator maleAnimator;
    [SerializeField] private Animator femAnimator;
    private void Awake()
    {
        EventManager.AddListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.AddListener<ArmorCollectEvent>(OnArmorCollect);
        EventManager.AddListener<BoosterMoneyCollectEvent>(OnBoosterCollect);
        EventManager.AddListener<BoosterWeaponCollectEvent>(OnBoosterCollect);
        EventManager.AddListener<BoosterSpeedCollectEvent>(OnBoosterCollect);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<MoneyCollectEvent>(OnMoneyCollect);
        EventManager.RemoveListener<ArmorCollectEvent>(OnArmorCollect);
        EventManager.RemoveListener<BoosterMoneyCollectEvent>(OnBoosterCollect);
        EventManager.RemoveListener<BoosterWeaponCollectEvent>(OnBoosterCollect);
        EventManager.RemoveListener<BoosterSpeedCollectEvent>(OnBoosterCollect);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);

    }

    private void OnGameOver(GameOverEvent obj)
    {
        maleAnimator.enabled = false;
        femAnimator.enabled = false;
        foreach ( var rigidbody in rigidbodies)
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }
    }

    private void OnBoosterCollect(BoosterEvent obj)
    {
        boosterCollectEffect.Play();
    }

    private void OnArmorCollect(ArmorCollectEvent obj)
    {
        armorCollectEffect.Play();
    }

    private void OnMoneyCollect(MoneyCollectEvent obj)
    {
        moneyCollectEffect.Play();
    }
}
