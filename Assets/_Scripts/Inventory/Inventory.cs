using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory: MonoBehaviour
{
    public static Inventory instance;
    
    public List<InventoryItem> equipment;
    public Dictionary<ItemDataEquipment, InventoryItem> equipmentDictionary;

    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventoryDictionary;

    [FormerlySerializedAs("stash")] public List<InventoryItem> stashItems;
    public Dictionary<ItemData, InventoryItem> stashDictionary;
    
    [Header("UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;
    
    
    private ItemSlotUI[] _inventoryItemSlot;
    private ItemSlotUI[] _stashItemSlot;
    private EquipmentSlotUI[] _equipmentSlotUI;

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

    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stashItems = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();

        _inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<ItemSlotUI>();
        _stashItemSlot = stashSlotParent.GetComponentsInChildren<ItemSlotUI>();
        _equipmentSlotUI = equipmentSlotParent.GetComponentsInChildren<EquipmentSlotUI>();
    }

    private void UpdateUISlot()
    {
        for (int i = 0; i < _equipmentSlotUI.Length; i++)
        {
            foreach (var itemEquipment in equipmentDictionary)
            {
                if (itemEquipment.Key.equipmentType == _equipmentSlotUI[i].equipmentType)
                {
                    _equipmentSlotUI[i].UpdateSlot(itemEquipment.Value);
                }
            }
        }
        
        for(var i = 0; i < _inventoryItemSlot.Length; i++)
        {
            _inventoryItemSlot[i].BersihkanSlot();
        }
        
        for(var i = 0; i < _stashItemSlot.Length; i++)
        {
            _stashItemSlot[i].BersihkanSlot();
        }


        
        for (var i = 0; i < inventoryItems.Count; i++)
        {
            _inventoryItemSlot[i].UpdateSlot(inventoryItems[i]);
        }
        
        for (var i = 0; i < stashItems.Count; i++)
        {
            _stashItemSlot[i].UpdateSlot(stashItems[i]);
        }
    }

    public void EquipItem(ItemData item)
    {
        ItemDataEquipment newEquipment = item as ItemDataEquipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemDataEquipment equipmentRemove = null;

        foreach (var itemEquipment in equipmentDictionary)
        {
            if (itemEquipment.Key.equipmentType == newEquipment.equipmentType)
            {
                equipmentRemove = itemEquipment.Key;
            }
        }

        if (equipmentRemove != null)
        {
            UnEquip(equipmentRemove);
            AddItem(equipmentRemove);
        }
            
        
        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();
        
        RemoveItem(item);
        
        UpdateUISlot();
        
    }

    public void UnEquip(ItemDataEquipment equipmentRemove)
    {
        if (equipmentRemove != null)
        {
            if (equipmentDictionary.TryGetValue(equipmentRemove, out InventoryItem value))
            {
                //AddItem(equipmentRemove);
                equipment.Remove(value);
                equipmentDictionary.Remove(equipmentRemove);
                equipmentRemove.RemoveModifiers();
            }
        }
    }

    public void AddItem(ItemData item)
    {

        if (item == null)
        {
            return;
        }
        
        if (item.itemType == ItemType.Equipment)
        {
            //AddToInventory(item);
            WhatItemToAdd(item, inventoryDictionary, inventoryItems);
        }
        else if (item.itemType == ItemType.Material)
        {
            //AddToStash(item);
            WhatItemToAdd(item, stashDictionary, stashItems);
        }
        
        
        UpdateUISlot();
    }

    private void WhatItemToAdd(ItemData item, Dictionary<ItemData, InventoryItem> dictionary,
        List<InventoryItem> whatItems)
    {
        if (dictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            whatItems.Add(newItem);
            dictionary.Add(item, newItem);
        }
    }

    private void AddToInventory(ItemData item)
    {
        if (inventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inventoryItems.Add(newItem);
            inventoryDictionary.Add(item, newItem);
        }
    }
    
    private void AddToStash(ItemData item)
    {
        if (stashDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            stashItems.Add(newItem);
            stashDictionary.Add(item, newItem);
        }
    }

    public void RemoveItem(ItemData item)
    {
        
        WhatItemToRemove(inventoryDictionary, inventoryItems, item);
        WhatItemToRemove(stashDictionary, stashItems, item);
        
        // if (inventoryDictionary.TryGetValue(item, out InventoryItem value))
        // {
        //     if (value.stackSize <= 1)
        //     {
        //         inventoryItems.Remove(value);
        //         inventoryDictionary.Remove(item);
        //     }
        //     else
        //     {
        //         value.RemoveStack();
        //     }
        // }
        //
        // if (stashDictionary.TryGetValue(item, out InventoryItem stashValue))
        // {
        //     if (stashValue.stackSize <= 1)
        //     {
        //         stashItems.Remove(stashValue);
        //         stashDictionary.Remove(item);
        //     }
        //     else
        //     {
        //         stashValue.RemoveStack();
        //     }
        // }
        UpdateUISlot();
    }

    private void WhatItemToRemove(Dictionary<ItemData, InventoryItem> dictionary, List<InventoryItem> whatItems,
        ItemData item)
    {
        if (dictionary.TryGetValue(item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                whatItems.Remove(value);
                dictionary.Remove(item);
            }
            else
            {
                value.RemoveStack();
            }
        }
    }
    
    

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ItemData item = inventoryItems[inventoryItems.Count - 1].ItemData;
            RemoveItem(item);
        }
    }
}