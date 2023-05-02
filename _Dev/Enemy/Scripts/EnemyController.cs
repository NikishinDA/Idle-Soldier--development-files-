using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyHealthManager enemyHealthManager;
    [SerializeField] private EnemyBulletShooter enemyShooter;
    [SerializeField] private EnemyEffectController enemyEffectController;
    [SerializeField] private GameObject enemyHitbox;
    [SerializeField] private float bountyMultiplier;

    private void Awake()
    {
        var evt = GameEventsHandler.EnemySpawnedEvent;
        evt.EnemyController = this;
        EventManager.Broadcast(evt);
    }

    public void Initialize(int playerDamage, int playerMaxArmor, float playerSpeed)
    {
        enemyHealthManager.Initialize(playerDamage, playerSpeed);
        enemyShooter.Initialize(playerMaxArmor);
        enemyHealthManager.EnemyDeathEvent += OnEnemyDeathEvent;
    }

    protected virtual void OnEnemyDeathEvent()
    {
        enemyShooter.SetActive(false);
        enemyEffectController.DeathEffect();
        var evt = GameEventsHandler.EnemyKilledEvent;
        evt.Transform = enemyHitbox.transform;
        enemyHitbox.SetActive(false);
        evt.Cost = (int) (bountyMultiplier * enemyShooter.GetDamage() * enemyHealthManager.GetMaxHealth());
        EventManager.Broadcast(evt);
        StartCoroutine(DelayDestroy(5f));
    }
    private IEnumerator DelayDestroy(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
