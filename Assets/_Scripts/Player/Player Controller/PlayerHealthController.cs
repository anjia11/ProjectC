using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 10;

    public float invicibility;
    private float invicicbleCounter;

    public float flashLength;
    private float flashCounter;
    public SpriteRenderer[] playerSprite;
    public GameObject deathEffect;

    public static PlayerHealthController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (invicicbleCounter > 0)
        {
            invicicbleCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                foreach (SpriteRenderer sr in playerSprite)
                {
                    sr.enabled = !sr.enabled;
                }

                flashCounter = flashLength;
            }
        }

        if (invicicbleCounter <= 0)
        {
            foreach (SpriteRenderer sr in playerSprite)
            {
                sr.enabled = true;
            }

            flashCounter = 0f;
        }
    }

    public void DamagePlayer(int damage)
    {
        if (invicicbleCounter <= 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (deathEffect != null)
                {
                    Instantiate(deathEffect, transform.position, transform.rotation);
                }

                //RespawnController.instance.Respawn();
            }
            else
            {
                invicicbleCounter = invicibility;
            }

            //UIController.instance.UpdateHealth(currentHealth, maxHealth);
        }
    }

    public void KeepHealthWhileDead()
    {
        currentHealth = maxHealth;
        //UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }

    public void HealthUp(int health)
    {
        currentHealth += health;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        //UIController.instance.UpdateHealth(currentHealth, maxHealth);
    }
}