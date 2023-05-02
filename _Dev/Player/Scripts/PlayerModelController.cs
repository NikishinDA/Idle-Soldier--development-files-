using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelController : MonoBehaviour
{
    [SerializeField] private GameObject girlModel;
    [SerializeField] private GameObject guyModel;
    private void Awake()
    {
        EventManager.AddListener<PlayerChangeModelRequestEvent>(OnModelChangeEvent);
        
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<PlayerChangeModelRequestEvent>(OnModelChangeEvent);
    }

    private void OnModelChangeEvent(PlayerChangeModelRequestEvent obj)
    {
        if(obj.Bin)
        {
            guyModel.SetActive(true);
            girlModel.SetActive(false);
        }
        else
        {
            guyModel.SetActive(false);
            girlModel.SetActive(true);
        }
        VarSaver.Bin = obj.Bin;
    }
}
