using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerDetector : MonoBehaviour
{
    private List<Transform> _targets;
    private void Awake()
    {
        _targets = new List<Transform>();
        EventManager.AddListener<EnemyKilledEvent>(OnEnemyKilled);
        EventManager.AddListener<PlayerModelChangeEvent>(OnPlayerModelChange);
    }


    private void OnDestroy()
    {
        EventManager.RemoveListener<EnemyKilledEvent>(OnEnemyKilled);

        EventManager.RemoveListener<PlayerModelChangeEvent>(OnPlayerModelChange);
    }

    private void OnPlayerModelChange(PlayerModelChangeEvent obj)
    {
        var evt = GameEventsHandler.PlayerTargetChangeEvent;
        evt.Target = _targets.Count > 0 ? _targets[0] : null;
        EventManager.Broadcast(evt);
    }
    private void OnEnemyKilled(EnemyKilledEvent obj)
    {
        _targets.Remove(obj.Transform);
        UpdateList();
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform enemyTransform = other.transform;
        _targets.Add(enemyTransform);
        UpdateList();
    }

    private void OnTriggerExit(Collider other)
    {
        _targets.Remove(other.transform);
        UpdateList();
    }
    private void UpdateList()
    {
        _targets = _targets.OrderBy(target => target.position.z).ToList();
        var evt = GameEventsHandler.PlayerTargetChangeEvent;
        evt.Target = _targets.Count > 0 ? _targets[0] : null;
        EventManager.Broadcast(evt);
    }
}
