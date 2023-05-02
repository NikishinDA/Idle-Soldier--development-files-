using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmorManager : MonoBehaviour
{
    [SerializeField] private GameObject[] malArmorPieces;
    [SerializeField] private GameObject[] femArmorPieces;
    private GameObject[] _armorPieces;
    private int _num;
    public void HealthUpdate(int health)
    {
        if (health <= 0) return;
        if (health == 1)
        {
            DisableArmor(malArmorPieces);
            DisableArmor(femArmorPieces);
            return;
        }
        int newNum = Mathf.Clamp(health, 2,malArmorPieces.Length + 1) - 2;
        int delta = newNum - _num;
        if (delta < 0)
        {
            for (int i = _num; i > newNum; i--)
            {
                malArmorPieces[i].SetActive(false);
                femArmorPieces[i].SetActive(false);
            }
        }
        else
        {
            for (int i = _num; i <= newNum; i++)
            {
                malArmorPieces[i].SetActive(true);
                femArmorPieces[i].SetActive(true);
            }
        }
        _num = newNum;
    }

    private void DisableArmor(GameObject[] pieces)
    {
        foreach (var piece in pieces)
        {
            piece.SetActive(false);
        }
        _num = 0;
    }
}
