using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour, IHitbox
{
    public void TakeDamage(int damage)
    {
        var evt = GameEventsHandler.PlayerTakeDamageEvent;
        evt.Damage = damage;
        EventManager.Broadcast(evt);
    }
}
