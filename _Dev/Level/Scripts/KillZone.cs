using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      EventManager.Broadcast(GameEventsHandler.GameOverEvent);
   }
}
