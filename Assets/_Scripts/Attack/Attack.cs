using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private CharacterStats _stats;
    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponentInParent<CharacterStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterStats target = other.GetComponent<CharacterStats>();
        if (target != null)
        {
            if (target.currentHealth > 0)
            {
                _stats.CalculateDamage(target);
            }
        }
    }
}
