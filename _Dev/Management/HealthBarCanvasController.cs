using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCanvasController : MonoBehaviour
{
    private Transform _mainCameraTransform;
    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    private void OnDestroy()
    {
    }

    void Update()
    {
        transform.forward = _mainCameraTransform.forward;
    }
}
