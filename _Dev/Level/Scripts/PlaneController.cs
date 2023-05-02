using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private Renderer _renderer;
    [SerializeField] private Material[] roadMaterials;
    [SerializeField] private Material[] borderMaterials;
    private void Awake()
    {
        EventManager.AddListener<AmbienceChangeEvent>(OnAmbienceChange);
        _renderer = GetComponent<Renderer>();
        ChangeMaterials();
    }

    private void ChangeMaterials()
    {
        Material[] materials = _renderer.materials;
        materials[1] = roadMaterials[VarSaver.AmbienceNumber];
        materials[0] = borderMaterials[VarSaver.AmbienceNumber];
        _renderer.materials = materials;
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<AmbienceChangeEvent>(OnAmbienceChange);

    }

    private void OnAmbienceChange(AmbienceChangeEvent obj)
    {
        ChangeMaterials();
    }
}
