using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour, IDamageable
{
    [Header("Major Stats")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;
    
    [Header("Defensif Stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    
    [Header("Offensive Stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critDamage;
    public Stat resistence;

    [Header("Elemental Stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat earthDamage;
    public Stat lightDamage;
    
    public int currentHealth;
    public static event Action onHealthChanged;

    private void OnEnable()
    {
        currentHealth = GetMaxHealthValue();
    }

    protected virtual void Start()
    {
        critDamage.SetDefaultValue(150);
    }

    public virtual void CalculateDamage(CharacterStats _targetStat)
    {
        if (CanAvoidDamage(_targetStat))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCritDamage(totalDamage);
            //Debug.Log("Total Crit Damage in int " + totalDamage);
        }

        totalDamage = CalculateDamageWithArmor(_targetStat, totalDamage);
        //Debug.Log("Total Damage dengan Armor =  " + totalDamage + " Armor = "+ _targetStat.armor.GetValue());
        _targetStat.TakeDamage(totalDamage);
        //Debug.Log("Damage == " + damage.GetValue());
        //Debug.Log("Kena Damage = " + totalDamage);
    }

    private static int CalculateDamageWithArmor(CharacterStats _targetStat, int totalDamage)
    {
        totalDamage -= _targetStat.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    public virtual void TakeDamage(int damage)
    {
        DecreseHealth(damage);

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void DecreseHealth(int damage)
    {
        currentHealth -= damage;
        onHealthChanged?.Invoke();
    }

    protected virtual void Die()
    {
        //Debug.Log("Mati");
    }
    
    bool CanAvoidDamage(CharacterStats _targetStat){
        int totalEvasion = _targetStat.evasion.GetValue() + _targetStat.agility.GetValue();
        if (Random.Range(0, 100) < totalEvasion)
        {
              //Debug.Log("ATTACK AVOIDED");
              return true;
        }

        return false;
    }

    private bool CanCrit()
    {
        int totalCritChance = critChance.GetValue() + agility.GetValue();

        if (Random.Range(1, 100) <= totalCritChance)
        {
            //Debug.Log("CRITTT");
            return true;
        }

        return false;
    }

    int CalculateCritDamage(int damage)
    {
        var totalCritDamage = critDamage.GetValue() * 0.01f;

        //Debug.Log("Total Crit Damage in % = " + critDamage.GetValue() +"%");
        var totalDamageWithCrit = damage + (damage * totalCritDamage);
        
        //Debug.Log("Total Crit Damage " + totalDamageWithCrit);

        return Mathf.RoundToInt(totalDamageWithCrit);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue();
    }

    public void DoDamage(int damage)
    {
        TakeDamage(damage);
    }

    public void DoDamage(CharacterStats characterStats)
    {
        CalculateDamage(characterStats);
    }
}

