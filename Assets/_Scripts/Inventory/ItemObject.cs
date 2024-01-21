using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private ItemData itemData;
    private SpriteRenderer _spriteRenderer;

    //digunakan untuk mengetahui nama atau apapun di hirarki.
    // private void OnValidate()
    // {
    //     SetItemVisual();
    // }

    private void SetItemVisual()
    {
        if (itemData == null)
            return;
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item - " + itemData.itemName;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Y))
    //     {
    //         _rigidbody2D.velocity = velocity;
    //     }
    // }

    public void SetUpItem(ItemData item, Vector2 velocity)
    {
        itemData = item;
        this.velocity = velocity;
        SetItemVisual();
        
        //Debug.Log("XXXX = "+velocity.x + " YYY = "+velocity.y);
    }

    public void PickUpItem()
    {
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
