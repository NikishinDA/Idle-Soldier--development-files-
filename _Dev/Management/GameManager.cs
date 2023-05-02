using System;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float _playTimer;
    private void Awake()
    {
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<PlayerCheckpointCrossEvent>(OnCheckPointCross);
        GameAnalytics.Initialize();
        
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        
        EventManager.RemoveListener<PlayerCheckpointCrossEvent>(OnCheckPointCross);
    }

    private void OnCheckPointCross(PlayerCheckpointCrossEvent obj)
    {
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.PlayedLevels, 0) + 1;
        var status = GAProgressionStatus.Complete;
        GameAnalytics.NewProgressionEvent(
            status,
            "Level_" + level,
            "PlayTime_" + Mathf.RoundToInt(_playTimer));
        GameAnalytics.NewProgressionEvent (
            GAProgressionStatus.Start,
            "Level_" + (level + 1));
        
        string level_id = "level_" + level;
        LevelFinishedResult finishedResult = LevelFinishedResult.win;
        HoopslyIntegration.Instance.RaiseLevelFinishedEvent(level_id, finishedResult);
        _playTimer = 0;
    }

    private void Start()
    {
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.PlayedLevels, 0) + 1;
        GameAnalytics.NewProgressionEvent (
            GAProgressionStatus.Start,
            "Level_" + level);
        string level_id = "level_" + level;
        bool measureFPS = true;
        HoopslyIntegration.Instance.RaiseLevelStartEvent(level_id, measureFPS);
        StartCoroutine(Timer());
    }

    private void OnGameOver(GameOverEvent obj)
    {
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.PlayedLevels, 0) + 1;
        var status =  GAProgressionStatus.Fail;
        GameAnalytics.NewProgressionEvent(
            status,
            "Level_" + level,
            "PlayTime_" + Mathf.RoundToInt(_playTimer));
        
        string level_id = "level_" + level;
        LevelFinishedResult finishedResult = LevelFinishedResult.lose;
        HoopslyIntegration.Instance.RaiseLevelFinishedEvent(level_id, finishedResult);
        
    }
    private IEnumerator Timer()
    {
        for (;;)
        {
            _playTimer += Time.deltaTime;
            yield return null;
        }
    }
#if  UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            EventManager.Broadcast(GameEventsHandler.MoneyCollectEvent);
        }
    }

    
#endif
    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
