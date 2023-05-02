using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChunkDetection : MonoBehaviour
{
    private Collider _trigger;
    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _trigger.enabled = false;
        EventManager.Broadcast(GameEventsHandler.PlayerFinishLevelEvent);
    }
}
