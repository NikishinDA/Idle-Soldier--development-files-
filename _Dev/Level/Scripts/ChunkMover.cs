using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    private ChunkManager _chunkManager;

    public void Initialize(ChunkManager chunkManager)
    {
        _chunkManager = chunkManager;
    }

    private void Start()
    {
        if (!_chunkManager)
        {
            _chunkManager = ChunkManager.Instance;
        }
    }

    private void LateUpdate()
    {
        transform.Translate(_chunkManager.Speed * Time.deltaTime * Vector3.back);
    }
}
