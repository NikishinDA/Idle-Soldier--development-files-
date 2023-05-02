using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

enum BoosterType
{
    Speed,
    Weapon,
    Money
}
public class BoosterDetection : MonoBehaviour
{
    [SerializeField] private GameObject[] boosters;
    private BoosterType _type;
    private void Awake()
    {
        _type = (BoosterType) Random.Range((int) BoosterType.Speed, (int) BoosterType.Money + 1);
        boosters[(int) _type].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        BoosterEvent evt;
        switch (_type)
        {
            case BoosterType.Speed:
                evt = GameEventsHandler.BoosterSpeedCollectEvent;
                evt.Toggle = true;
                EventManager.Broadcast(evt);
                break;
            case BoosterType.Weapon:
                evt = GameEventsHandler.BoosterWeaponCollectEvent;
                evt.Toggle = true;
                EventManager.Broadcast(evt);
                break;
            case BoosterType.Money:
                evt = GameEventsHandler.BoosterMoneyCollectEvent;
                evt.Toggle = true;
                EventManager.Broadcast(evt);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Destroy(transform.parent.gameObject);
    }
}
