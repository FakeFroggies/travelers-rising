using UnityEngine;
using Photon.Pun;

public class PlayerHealthMP : MonoBehaviour, Damageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private HealthBar healthbar;
    [SerializeField] private PlayerData playerData;
    public GameObject player;
    PhotonView view;
    
    private int health;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
                Die();
            }
            healthbar.UpdateHealthBar(maxHealth, health);
            if (PhotonNetwork.IsConnected)
            {
                view.RPC("UpdateHealth", RpcTarget.Others, health);
            }
        }
    }
    
    public void Start()
    {
        view = GetComponent<PhotonView>();
        Health = maxHealth;
    }

    private void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                TakeDamage(10);
            }
        }
    }

    [PunRPC]
    private void UpdateHealth(int newHealth)
    {
        health = newHealth;
        healthbar.UpdateHealthBar(maxHealth, health);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void Die()
    {
        Health = maxHealth;
        player.transform.position = new Vector3(0, 5, 0);
    }
}