using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    private int _ambienceNumber;
    [SerializeField] private Material[] skyboxMaterials;

    public int AmbienceNumber { get => _ambienceNumber; }
    public Action AmbienceChange;

    private void Awake()
    {
        _ambienceNumber = PlayerPrefs.GetInt(PlayerPrefsStrings.Ambience, 0);
        EventManager.AddListener<PlayerCheckpointCrossEvent>(OnCheckpointCross);
    }
    private void OnDestroy()
    {

        EventManager.RemoveListener<PlayerCheckpointCrossEvent>(OnCheckpointCross);
    }
    private void Start()
    {
        UpdateAmbience();
    }
    private void OnCheckpointCross(PlayerCheckpointCrossEvent obj)
    {
        if (obj.Section == 2)
        {
            _ambienceNumber++;
            UpdateAmbience();
        }
        Debug.Log(obj.Section);
        Debug.Log(_ambienceNumber);
    }

    private void UpdateAmbience()
    {
        if (_ambienceNumber == 3)
        {
            _ambienceNumber = 0;
        }
        RenderSettings.skybox = skyboxMaterials[_ambienceNumber];
        AmbienceChange.Invoke();
        PlayerPrefs.SetInt(PlayerPrefsStrings.Ambience, _ambienceNumber);
        VarSaver.AmbienceNumber = _ambienceNumber;
        EventManager.Broadcast(GameEventsHandler.AmbienceChangeEvent);
    }
}
