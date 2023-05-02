using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField] private int health;
    protected int _maxHealth;
    public event Action EnemyDeathEvent;

    protected int Health
    {
        get => health;
        set
        {
            health = value;
            indicatorController.UpdateHealth(value);
        }
    }

    [SerializeField] private HealthIndicatorController indicatorController;



    private void Start()
    {
        indicatorController.UpdateHealth(Health);
    }

    public virtual void Initialize(int playerDamage, float playerSpeed)
    {
        Health = Mathf.RoundToInt(Random.Range(0.5f, 1.25f) * playerDamage);
        _maxHealth = Health;
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            SafeDestroy();
        }
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
    protected virtual void SafeDestroy()
    {
        EnemyDeathEvent?.Invoke();
        indicatorController.gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
