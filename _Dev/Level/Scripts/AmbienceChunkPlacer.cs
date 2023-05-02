using System.Collections.Generic;
using UnityEngine;

public class AmbienceChunkPlacer : MonoBehaviour
{
    [SerializeField] private Chunk[] chunkPrefabs;
    [SerializeField] private Chunk[] firstPrefab;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform despawnPoint;
    
    private List<Chunk> _spawnedChunks;
    private bool _finishSpawned;
    [SerializeField] private ChunkManager _chunkManager;
    private Chunk _currentChunk;
    [SerializeField] private AmbienceManager ambienceManager;

    [SerializeField] private float nextLevelStartZ;
    private void Awake()
    {
        _currentChunk = chunkPrefabs[VarSaver.AmbienceNumber];
        _spawnedChunks = new List<Chunk>();
        ambienceManager.AmbienceChange += OnAmbienceChange;
        /*foreach (Chunk ch in firstPrefab)
        {
            //ch.SetEnv(_level);
            _spawnedChunks.Add(ch);
            ch.Initialize(_chunkManager);
        }*/
        SpawnFirst();
    }
    private void SpawnFirst()
    {
        Chunk newChunk = Instantiate(_currentChunk, chunkParent);
        newChunk.transform.position =
           despawnPoint.position - newChunk.begin.localPosition;
        _spawnedChunks.Add(newChunk);
        newChunk.Initialize(_chunkManager);
    }
    private void Start()
    {
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
    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(_currentChunk, chunkParent);
        newChunk.transform.position =
            _spawnedChunks[_spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
        _spawnedChunks.Add(newChunk);
        newChunk.Initialize(_chunkManager);
    }
    private void OnAmbienceChange()
    {
        foreach (var chunk in _spawnedChunks)
        {
            Destroy(chunk.gameObject);
        }
        _spawnedChunks.Clear();
        _currentChunk = chunkPrefabs[ambienceManager.AmbienceNumber];
        SpawnFirst();
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
}
