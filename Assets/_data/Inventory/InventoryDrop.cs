using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDrop : InventoryAbstract
{
   // [Header("Item Drop")]

    protected override void Start()
    {
        base.Start();
    }


    protected virtual void DropItemIndex(int itemIndex)
    {
        ItemInventory itemInventory = this.inventory.Items[itemIndex];
        Vector3 droPos = transform.position;
        droPos.x += 1;
        ItemDropSpawner.Instance.DropByInventory(itemInventory, droPos, transform.rotation);
        this.inventory.Items.Remove(itemInventory);
    }
}
