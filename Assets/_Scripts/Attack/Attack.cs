using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Entities;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private CharacterStats _stats;

    private int moveDir;
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
            if (target.transform.position.x > transform.parent.position.x)
            {
                moveDir = 1;
            }else if (target.transform.position.x < transform.parent.position.x)
            {
                moveDir = -1;
            }
            if (target.currentHealth > 0)
            {
                target.GetComponent<Entity>().DamageFX(moveDir);
                _stats.CalculateDamage(target);
            }
        }
    }
}
