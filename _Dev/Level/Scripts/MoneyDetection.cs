using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.Broadcast(GameEventsHandler.MoneyCollectEvent);
        Destroy(transform.parent.gameObject);
    }
}
