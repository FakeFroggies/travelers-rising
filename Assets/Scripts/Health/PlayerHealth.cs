using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, Damageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private HealthBar healthbar;
    public GameObject objectToShow;
    public int health;
    public int Health 
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
            healthbar.UpdateHealthBar(maxHealth, health);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(10);
        }
    }
    public void Die()
    {
        objectToShow.SetActive(true);
        Time.timeScale = 0;
    }

    public void TakeDamage(int value)
    {
        Health -= value;
    }
    
    public void Start()
    {
        Health = maxHealth;
    }
}