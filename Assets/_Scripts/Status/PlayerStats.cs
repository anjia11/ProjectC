using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player _player;

    public static PlayerStats instance;

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
    protected override void Start()
    {
        base.Start();
        _player = GetComponent<Player>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth >= 0)
            _player.DamageFX();
    }

    protected override void Die()
    {
        base.Die();
        _player.Die();
    }
}