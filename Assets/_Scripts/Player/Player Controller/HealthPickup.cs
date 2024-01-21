using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private GameObject healEffect;
    [SerializeField] int heal = 2;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (healEffect != null)
            {
                Instantiate(healEffect, transform.position, transform.rotation);
            }
            PlayerHealthController.instance.HealthUp(heal);
            Destroy(gameObject);
        }
    }
}
