using System;
using _Scripts.Entities;
using UnityEngine;

public class Meleduk : MonoBehaviour
{
    private CharacterStats _stats;
    private int moveDir;

    private void Start()
    {
        _stats = GameObject.FindGameObjectWithTag("Meledak").GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterStats target = other.GetComponent<CharacterStats>();
        if (target != null)
        {
            if (target.transform.position.x > transform.position.x)
            {
                moveDir = 1;
            }else if (target.transform.position.x < transform.position.x)
            {
                moveDir = -1;
            }
            target.GetComponent<Entity>().DamageFX(moveDir);
            _stats.CalculateDamage(target);
        }
    }
}