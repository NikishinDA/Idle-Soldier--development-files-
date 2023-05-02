using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class ChunkPreview : MonoBehaviour
{
    [Header("Chunk Manager")] [SerializeField]
    private ChunkManager chunkManager;
    [SerializeField] private Chunk[] firstPrefab;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private Chunk[] chunksToSpawn;
    [Header("Build")] [SerializeField] private bool build;
    [SerializeField] private bool rebuild;

    private void Update()
    {
        if (build)
        {
            build = false;
            BuildLevel();
        }

        if (rebuild)
        {
            rebuild = false;
            if (chunkParent)
                DestroyImmediate(chunkParent.gameObject);
            BuildLevel();
        }
    }

    private void BuildLevel()
    {
        List<Chunk> spawnedChunks = firstPrefab.ToList();
        if (!chunkParent)
        {
            chunkParent = new GameObject().transform;
        }

        foreach (var chunk in chunksToSpawn)
        {
            var newChunk = Instantiate(chunk, chunkParent);
            newChunk.transform.position =
                spawnedChunks[spawnedChunks.Count - 1].end.position - newChunk.begin.localPosition;
            spawnedChunks.Add(newChunk);
            newChunk.Initialize(chunkManager);
        }
    }
}