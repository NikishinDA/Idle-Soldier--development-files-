using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointDetection : MonoBehaviour
{
    private Collider _trigger;
    [SerializeField] private ParticleSystem _effect;
    private void Awake()
    {
        _trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _trigger.enabled = false;
        var evt = GameEventsHandler.PlayerCheckpointCrossEvent;
        evt.Section = VarSaver.SectionNumber;
        EventManager.Broadcast(evt);
        
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.PlayedLevels, 0);
        PlayerPrefs.SetInt(PlayerPrefsStrings.PlayedLevels, level + 1);
        PlayerPrefs.Save();
        if (VarSaver.SectionNumber == 0)
            _effect.Play();
    }
}
