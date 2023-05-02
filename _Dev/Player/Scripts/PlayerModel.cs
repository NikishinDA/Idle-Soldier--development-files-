using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] private bool bin;
    private void OnEnable()
    {
        var evt = GameEventsHandler.PlayerModelChangeEvent;
        evt.Bin = bin;
        EventManager.Broadcast(evt);
    }
}
