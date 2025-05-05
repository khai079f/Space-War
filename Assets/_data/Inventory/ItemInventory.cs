using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemInventory 
{
    public string itemId;
    public ItemProfileSO itemProfile;
    public int itemCount = 0;
    public int maxStack = 7;
    public int upgradeLevel = 0;

    public virtual ItemInventory Clone()
    {
        ItemInventory item = new ItemInventory
        {
            itemId = ItemInventory.RandomId(),
            itemProfile = this.itemProfile,
            itemCount = this.itemCount,
            maxStack = this.maxStack,
            upgradeLevel = this.upgradeLevel,
    };
        return item;

    }
    public static string RandomId()
    {
        return Guid.NewGuid().ToString();
    }
}
