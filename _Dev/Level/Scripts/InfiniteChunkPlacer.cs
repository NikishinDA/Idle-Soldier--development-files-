using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
class Level
{
    public Chunk[] chunks;
}
public class InfiniteChunkPlacer : MonoBehaviour
{
    [SerializeField] private Chunk chunkPrefab;
    [SerializeField] private Chunk checkPointChunkPrefab;
    [SerializeField] private Chunk[] firstPrefab;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform despawnPoint;
    [SerializeField] private Chunk endChunk;
    [SerializeField] private float nextLevelStartZ;
    private List<Chunk> _spawnedChunks;
    private bool _finishSpawned;
    private int _level;
    private ChunkManager _chunkManager;
    private bool _checkPointNext;
    private bool _onlyStraight;
    [SerializeField] private Level[] levels;
    private List<Chunk> _chunks;

    private void Awake()
    {
        _spawnedChunks = new List<Chunk>();
        EventManager.AddListener<GameOverEvent>(OnGameOver);
        EventManager.AddListener<GameStartEvent>(OnGameStart);
        EventManager.AddListener<BossSpawnEvent>(OnBossSpawn);
        EventManager.AddListener<BossDeathEvent>(OnBossDeath);
        _chunkManager = GetComponent<ChunkManager>();
        SetLevelChunks(PlayerPrefs.GetInt(PlayerPrefsStrings.PlayedLevels, 0));
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<GameOverEvent>(OnGameOver);
        EventManager.RemoveListener<GameStartEvent>(OnGameStart);
        EventManager.RemoveListener<BossSpawnEvent>(OnBossSpawn);
        EventManager.RemoveListener<BossDeathEvent>(OnBossDeath);

    }

    private void OnBossSpawn(BossSpawnEvent obj)
    {
        _onlyStraight = true;
    }

    private void OnBossDeath(BossDeathEvent obj)
    {
        CutExcess();
        _checkPointNext = true;
        _onlyStraight = false;
        
        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 0);
        PlayerPrefs.SetInt(PlayerPrefsStrings.Level, level + 1);
        PlayerPrefs.Save();
        SetLevelChunks(level+1);
    }

    private void CutExcess()
    {
        bool excessExist = true;
        while (excessExist)
        {
            if (_spawnedChunks[_spawnedChunks.Count - 1].end.position.z > nextLevelStartZ)
            {
                Destroy(_spawnedChunks[_spawnedChunks.Count - 1].gameObject);
                _spawnedChunks.RemoveAt(_spawnedChunks.Count - 1);
            }
            else
            {
                excessExist = false;
            }
        }
    }
    private void OnGameStart(GameStartEvent obj)
    {
    }

    private void Start()
    {
        foreach (Chunk ch in firstPrefab)
        {
            //ch.SetEnv(_level);
            _spawnedChunks.Add(ch);
            ch.Initialize(_chunkManager);
        }

        InitialSpawn();
    }

    private void Update()
    {
        if ((!_finishSpawned) &&
            (_spawnedChunks[_spawnedChunks.Count - 1].end.position.z < spawnPoint.position.z))
        {
            SpawnChunk();
        }

        if (_spawnedChunks[0].end.position.z < despawnPoint.position.z)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }
    }

    private void InitialSpawn()
    {
        while (_spawnedChunks[_spawnedChunks.Count - 1].end.position.z < spawnPoint.position.z)
        {
            SpawnChunk();
        }

        EventManager.Broadcast(GameEventsHandler.LevelPreparedEvent);
    }

    private void SpawnChunk()
    {
        Chunk newChunk;
        if (_onlyStraight)
        {
            newChunk = Instantiate(chunkPrefab, chunkParent);
        }
        else
        {
            if (_checkPointNext)
            {
                newChunk = Instantiate(checkPointChunkPrefab, chunkParent);
                _checkPointNext = false;
            }
            else
            {
                Chunk nextChunk;
                if (TryGetNextChunk(out nextChunk))
                    newChunk = Instantiate(nextChunk, chunkParent);
                else
                {
                    if (VarSaver.SectionNumber == 0)
                    {
                        newChunk = Instantiate(checkPointChunkPrefab, chunkParent);
                        int level = PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 0);
                        PlayerPrefs.SetInt(PlayerPrefsStrings.Level, level + 1);
                        PlayerPrefs.Save();
                        SetLevelChunks(level + 1);
                    }
                    else
                    {

                        newChunk = Instantiate(endChunk, chunkParent);
                        _onlyStraight = true;
                    }
                    
                }
            }
        }

        newChunk.transform.position =
            _spawnedChunks[_spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
        _spawnedChunks.Add(newChunk);
        newChunk.Initialize(_chunkManager);
        /* if (_spawnedChunks.Count > concurrentChunkNumber)
         {
             Destroy(_spawnedChunks[0].gameObject);
             _spawnedChunks.RemoveAt(0);
         }*/
    }

    private bool TryGetNextChunk(out Chunk nextChunk)
    {
        if (_chunks.Count > 0)
        {
            nextChunk = _chunks[0];
            _chunks.RemoveAt(0);
            return true;
        }
        else
        {
            nextChunk = chunkPrefab;
            return false;
        }
    }

    private void SetLevelChunks(int levelNum)
    {
        _level = levelNum; //PlayerPrefs.GetInt(PlayerPrefsStrings.Level, 0);
        if (_level >= levels.Length)
        {
            _level = Random.Range(1, levels.Length);
        }

        _chunks = levels[_level].chunks.ToList();
        VarSaver.LevelLength = _chunks.Count;
        
        Debug.Log(_level);
    }
    private void OnGameOver(GameOverEvent obj)
    {
        _finishSpawned = true;
    }
}