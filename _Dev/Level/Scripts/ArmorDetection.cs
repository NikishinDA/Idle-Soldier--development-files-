using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EventManager.Broadcast(GameEventsHandler.ArmorCollectEvent);
        Destroy(transform.parent.gameObject);
    }
}
