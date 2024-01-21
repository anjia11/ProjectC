

using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Ring,
    Necklace
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemDataEquipment: ItemData
{
    public EquipmentType equipmentType;
    
    [Header("Major Stats")]
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;
    
    [Header("Defensif Stats")]
    public int maxHealth;
    public int armor;
    public int evasion;
    
    [Header("Offensive Stats")]
    public int damage;
    public int critChance;
    public int critDamage;
    public int resistence;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance._player.GetComponent<PlayerStats>();
        
        playerStats.strength.Addmodifier(strength);
        playerStats.agility.Addmodifier(agility);
        playerStats.intelligence.Addmodifier(intelligence);
        playerStats.vitality.Addmodifier(vitality);
        
        playerStats.maxHealth.Addmodifier(maxHealth);
        playerStats.armor.Addmodifier(armor);
        playerStats.evasion.Addmodifier(evasion);
        
        playerStats.damage.Addmodifier(damage);
        playerStats.critChance.Addmodifier(critChance);
        playerStats.critDamage.Addmodifier(critDamage);
        playerStats.resistence.Addmodifier(resistence);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance._player.GetComponent<PlayerStats>();
        
        playerStats.strength.RemoveModifier(strength);
        playerStats.agility.RemoveModifier(agility);
        playerStats.intelligence.RemoveModifier(intelligence);
        playerStats.vitality.RemoveModifier(vitality);
        
        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.armor.RemoveModifier(armor);
        playerStats.evasion.RemoveModifier(evasion);
        
        playerStats.damage.RemoveModifier(damage);
        playerStats.critChance.RemoveModifier(critChance);
        playerStats.critDamage.RemoveModifier(critDamage);
        playerStats.resistence.RemoveModifier(resistence);
    }
    
}