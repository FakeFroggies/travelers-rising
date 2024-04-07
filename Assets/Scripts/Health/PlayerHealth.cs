using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, Damageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private HealthBar healthbar;
    [SerializeField] private PlayerData playerData;
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

    public void Start()
    {
        Health = maxHealth;
    }

    [Obsolete]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Load();
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

    [ContextMenu("Save")]
    [Obsolete]
    public void Save()
    {
        playerData.X = transform.position.x;
        playerData.Y = transform.position.y;
        playerData.Z = transform.position.z;

        playerData.HP = health;

        SaveLoad.SaveData(playerData, "PlayerData");
    }

    [ContextMenu("Load")]
    [Obsolete]
    public async void Load()
    {
        var data = await SaveLoad.LoadData<PlayerData>("PlayerData");
        transform.position = new Vector3(data.X, data.Y, data.Z);
        health = data.HP;
        healthbar.UpdateHealthBar(maxHealth, health);
    }
}

[Serializable]
public struct PlayerData
{
    public int Level;
    public int HP;
    public string Name;
    public float X, Y, Z;
}