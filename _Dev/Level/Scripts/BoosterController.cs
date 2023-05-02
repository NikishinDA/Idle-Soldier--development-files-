using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private void Start()
    {
        transform.SetParent(null);
    }
    private void Update()
    {
        transform.Translate(0, 0, - speed * Time.deltaTime);
    }
}
