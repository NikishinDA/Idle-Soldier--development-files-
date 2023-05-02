using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private Vector3 endPos;
    private void Awake()
    {
        StartCoroutine(SetInPosCor(4f));
    }

    private IEnumerator SetInPosCor(float time)
    {
        Vector3 startPos = transform.position;
        startPos.y = 0;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t / time);
            yield return null;
        }

        transform.position = endPos;
    }
    
}
