using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotUI : ItemSlotUI
{
    public EquipmentType equipmentType;

    private void OnValidate()
    {
        gameObject.name = "Equipment Type - " + equipmentType;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item.ItemData != null)
        {
            Inventory.instance.UnEquip(item.ItemData as ItemDataEquipment);
            Inventory.instance.AddItem(item.ItemData as ItemDataEquipment);
            BersihkanSlot();
        }
        // unequip
        
    }
}
