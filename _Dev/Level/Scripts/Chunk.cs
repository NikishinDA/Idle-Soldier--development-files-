using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Chunk : MonoBehaviour
{
    public Transform begin;
    public Transform end;
    private ChunkMover _chunkMover;

    private void Awake()
    {
        _chunkMover = GetComponent<ChunkMover>();
    }

    public void Initialize(ChunkManager chunkManager)
    {
        _chunkMover = GetComponent<ChunkMover>();
        _chunkMover.Initialize(chunkManager);
    }
}