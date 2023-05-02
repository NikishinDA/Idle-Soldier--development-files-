using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicatorController : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text healthText;
    public void UpdateHealth(int value)
    {
        healthText.text = value.ToString();
    }
}
