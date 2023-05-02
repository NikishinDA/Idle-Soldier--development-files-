using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject loseScreen;
   private void Awake()
   {
      EventManager.AddListener<GameOverEvent>(OnGameOver);
   }
    private void Start()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsStrings.FirstLaunch, 1) == 1)
        {
            startScreen.SetActive(true);
            PlayerPrefs.SetInt(PlayerPrefsStrings.FirstLaunch, 0);
            PlayerPrefs.Save();
        }
    }

    private void OnDestroy()
   {
      EventManager.RemoveListener<GameOverEvent>(OnGameOver);
   }

   private void OnGameOver(GameOverEvent obj)
   {
      loseScreen.SetActive(true);
   }
}
