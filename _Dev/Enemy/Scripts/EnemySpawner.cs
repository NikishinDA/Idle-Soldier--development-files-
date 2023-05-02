using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float cooldownTime;
    [SerializeField] private float spawnBorder;
    private float _cd;
    private bool _isActive;
    private Vector3 _raycastPos;
    private Vector3 _spawnPos;
    private RaycastHit _raycastHit;
    [SerializeField] private LayerMask layerMask;
    private Color _debugColor;
    [SerializeField] private float timeTillBoss;
    private float _bossCD;
    [SerializeField] private BossAimController miniBossPrefab;
    [SerializeField] private BossAimController[] bossPrefabs;
    [SerializeField] private Transform playerTransform;
    
    private void Awake()
    {
        _cd = cooldownTime;
        _raycastPos = transform.position;
        _bossCD = timeTillBoss;
        EventManager.AddListener<LevelPreparedEvent>(OnLevelPrepared);
        EventManager.AddListener<PlayerCheckpointCrossEvent>(OnCheckpointCross);
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        //EventManager.AddListener<LevelFinishedSpawningEvent>(OnLevelFinishedSpawning);
        EventManager.AddListener<PlayerFinishLevelEvent>(OnPlayerFinish);
    }

    private void OnDestroy()
    {
        
        EventManager.RemoveListener<LevelPreparedEvent>(OnLevelPrepared);
        EventManager.RemoveListener<PlayerCheckpointCrossEvent>(OnCheckpointCross);
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        //EventManager.RemoveListener<LevelFinishedSpawningEvent>(OnLevelFinishedSpawning);
        EventManager.RemoveListener<PlayerFinishLevelEvent>(OnPlayerFinish);


    }

    private void OnPlayerFinish(PlayerFinishLevelEvent obj)
    {
        BossAimController bossObject;
        if (VarSaver.SectionNumber == 1)
        {
            bossObject = miniBossPrefab;
        }
        else if (VarSaver.SectionNumber == 2)
        {
            bossObject = bossPrefabs[Random.Range(0, bossPrefabs.Length)];
        }
        else
        {
            throw new Exception("Boss does not need to spawm, yet spawn was requested");
        }
        Instantiate(bossObject, transform.position, Quaternion.LookRotation(Vector3.back)).SetTarget(playerTransform);
        EventManager.Broadcast(GameEventsHandler.BossSpawnEvent);
    }


    private void OnGameOver(GameOverEvent obj)
    {
        _isActive = false;
    }

    private void OnCheckpointCross(PlayerCheckpointCrossEvent obj)
    {
        _isActive = true;
        _cd = cooldownTime;
        _bossCD = timeTillBoss;
    }


    private void OnLevelPrepared(LevelPreparedEvent obj)
    {
        _isActive = true;
    }
    /*
    private IEnumerator DelaySpawnCor(float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            yield return null;
        }
        Instantiate(bossPrefab, transform.position, Quaternion.LookRotation(Vector3.back)).SetTarget(playerTransform);
        EventManager.Broadcast(GameEventsHandler.BossSpawnEvent);
    }*/
}
