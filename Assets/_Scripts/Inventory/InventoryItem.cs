using System;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData ItemData;
    public int stackSize;

    public InventoryItem(ItemData newItemData)
    {
        ItemData = newItemData;
        AddStack();
    }

    public void AddStack()
    {
        stackSize++;
    }

    public void RemoveStack()
    {
        stackSize--;
    }
}