using System;
using UnityEngine;

public class Meleduk : MonoBehaviour
{
    private CharacterStats _stats;

    private void Start()
    {
        _stats = GameObject.FindGameObjectWithTag("Meledak").GetComponent<CharacterStats>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CharacterStats target = other.gameObject.GetComponent<CharacterStats>();
        if (target != null)
        {
            _stats.CalculateDamage(target);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        CharacterStats target = other.GetComponent<CharacterStats>();
        if (target != null)
        {
            _stats.CalculateDamage(target);
        }
    }
}