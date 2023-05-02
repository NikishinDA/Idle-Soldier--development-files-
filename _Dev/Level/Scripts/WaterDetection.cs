using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var evt = GameEventsHandler.SlowDownEvent;
        evt.Toggle = true;
        EventManager.Broadcast(evt);
    }

    private void OnTriggerExit(Collider other)
    {
        var evt = GameEventsHandler.SlowDownEvent;
        evt.Toggle = false;
        EventManager.Broadcast(evt);
    }
}
