using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryManager inventoryManager;
    public Item[] ItemToPick;


    public void PickItem(int id){
        if (id >= 0 && id <= 5) // Only allow IDs for items, not details
        {
            inventoryManager.AddItem(ItemToPick[id], id);
            int detailId = id + 6;
        }
    }
    public Item Deneme(GameObject parent, int id){
        return ItemToPick[id];        
    }

}
