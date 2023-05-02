using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthManager : EnemyHealthManager
{
    [SerializeField] private int healthOffset;
    [SerializeField] private int healthAddition;
    public override void Initialize(int playerDamage, float playerSpeed)
    {
        Health = (Random.Range(-healthOffset, healthOffset) + healthAddition) * playerDamage;
        _maxHealth = Health;
    }

    protected override void SafeDestroy()
    {
        base.SafeDestroy();
        EventManager.Broadcast(GameEventsHandler.BossDeathEvent);
    }
}
