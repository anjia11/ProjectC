using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int jumlahItems;
    [SerializeField] private ItemData[] items;
    private List<ItemData> dropList = new List<ItemData>();
    
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData item;


    public void RandomDrop()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Random.Range(0, 100) < items[i].dropChance)
            {
                dropList.Add(items[i]);
            }
        }
        //Debug.Log("drop list ==== " +dropList.Count);
        // foreach (var v in dropList)
        // {
        //     Debug.Log("drop list  Name ==== " + v.itemName);
        // }
        

        for (int i = 0; i < jumlahItems-1; i++)
        {
            if (dropList.Count > 0)
            {
                ItemData random = dropList[Random.Range(0, dropList.Count - 1)];
                //Debug.Log("RANDOMM ITEM DROP = " + random.itemName);
                dropList.Remove(random);
                DropItem(random);
            }
        }
    }

    private void DropItem(ItemData itemData)
    {
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(20, 30), Random.Range(30, 37));
        newDrop.GetComponent<ItemObject>().SetUpItem(itemData, randomVelocity);
    }
}