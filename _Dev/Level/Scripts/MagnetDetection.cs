using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.Broadcast(GameEventsHandler.MagnetActivationEvent);
    }
}
