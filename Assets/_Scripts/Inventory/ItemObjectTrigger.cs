using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    private ItemObject _itemObject => GetComponentInParent<ItemObject>();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            
            //Debug.Log("APAKAH BISA");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            _itemObject.PickUpItem();
            //Debug.Log("PICK");
        }
    }
}
