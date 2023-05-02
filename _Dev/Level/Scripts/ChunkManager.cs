using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float slowDownEffect;
    public static ChunkManager Instance;
    [SerializeField] private float speedUpEffect;
    public float Speed
    {
        get => speed * _slowDownSpeedModifier * _speedUpModifier;
    }

    private float _slowDownSpeedModifier = 1f;
    private float _speedUpModifier = 1f;
    private IEnumerator _effectCor;
    private void Awake()
    {
        Instance = this;
        EventManager.AddListener<SlowDownEvent>(OnSlowDown);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<BoosterSpeedCollectEvent>(OnSpeedBoosterPickUp);
        
        var evt = GameEventsHandler.LevelSpeedChangeEvent;
        evt.Speed = speed;
        EventManager.Broadcast(evt);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<SlowDownEvent>(OnSlowDown);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<BoosterSpeedCollectEvent>(OnSpeedBoosterPickUp);

    }

    private void OnSpeedBoosterPickUp(BoosterSpeedCollectEvent obj)
    {
        if (obj.Toggle)
        {
            _speedUpModifier = speedUpEffect;
        }
        else
        {
            _speedUpModifier = 1f;
        }
    }

    private void OnGameOver(GameOverEvent obj)
    {
        _slowDownSpeedModifier = 0f;
    }

    private void OnSlowDown(SlowDownEvent obj)
    {
        if (obj.Toggle)
        {
            _slowDownSpeedModifier = slowDownEffect;
        }
        else
        {
            _slowDownSpeedModifier = 1f;
        }
    }


}
