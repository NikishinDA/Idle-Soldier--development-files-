using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderManager : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private Image actionButtonImage;
    [SerializeField] private Sprite femGraph;
    [SerializeField] private Sprite maleGraph;
    private bool _bin;
    private void Awake()
    {
        actionButton.onClick.AddListener(OnActivateButton);
        _bin = PlayerPrefs.GetInt(PlayerPrefsStrings.Gen, 1) == 1;
        VarSaver.Bin = _bin;
        SwitchGrafics();
    }
    private void Start()
    {
        
        var evt = GameEventsHandler.PlayerChangeModelRequestEvent;
        evt.Bin = _bin;
        EventManager.Broadcast(evt);
    }
    private void OnActivateButton()
    {
        SwitchGrafics();
        _bin = !_bin;
        var evt = GameEventsHandler.PlayerChangeModelRequestEvent;
        evt.Bin = _bin;
        EventManager.Broadcast(evt);
        PlayerPrefs.SetInt(PlayerPrefsStrings.Gen, _bin ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void SwitchGrafics()
    {
        if (!_bin)
        {
            actionButtonImage.sprite = femGraph;
        }
        else
        {
            actionButtonImage.sprite = maleGraph;
        }
    }

}
