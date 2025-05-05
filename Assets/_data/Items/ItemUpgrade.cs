using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrade : InventoryAbstract
{
    [Header("Item Upgrade")]
    [SerializeField] protected int maxLevel = 9;

/*    protected override void Start()
    {
        base.Start();
        Invoke(nameof(this.Test), 2);
        Invoke(nameof(this.Test), 3);
        Invoke(nameof(this.Test), 4);
    }
*/
    protected virtual void Test()
    {
        this.UpgradeItem(0);
    }

    protected virtual bool UpgradeItem(int itemIndex)
    {
        if (itemIndex >= this.inventory.Items.Count) return false;
        ItemInventory itemInventory = this.inventory.Items[itemIndex];
        if (itemInventory.itemCount < 1) return false;
        List<ItemRecipe> upgradeLevels = itemInventory.itemProfile.upgradeLevels;

        if (!this.ItemUpgradeable(upgradeLevels)) return false;

        if (!this.HaveEnoughIngredients(upgradeLevels,itemInventory.upgradeLevel)) return false;

        this.DeductIngredients(upgradeLevels, itemInventory.upgradeLevel);
        itemInventory.upgradeLevel++;
        return true;

    }

    protected virtual bool ItemUpgradeable(List<ItemRecipe> upgradeLevels)
    {
        if (upgradeLevels.Count == 0) return false;
        return true;
    }

    protected virtual bool HaveEnoughIngredients(List<ItemRecipe> upgradeLevels, int currrentLevel)
    {
        ItemCode itemCode;
        int itemCount;

        if (currrentLevel > upgradeLevels.Count)
        {
            Debug.LogWarning("Item can't upgrade anymore,currrentLevel: " + currrentLevel);
            return false;
        }
        ItemRecipe currentRecipeLevel = upgradeLevels[currrentLevel];
        foreach(ItemRecipeIngredient ingredient in currentRecipeLevel.ingredient)
        {
            itemCode = ingredient.itemProfile.itemCode;
            itemCount = ingredient.itemCount;
            if (!this.inventory.ItemCheck(itemCode, itemCount)) return false;
        }

        return true;
    }

    protected virtual void DeductIngredients(List<ItemRecipe> upgradeLevels, int currrentLevel)
    {
        ItemCode itemCode;
        int itemCount;

        ItemRecipe currentRecipeLevel = upgradeLevels[currrentLevel];
        foreach (ItemRecipeIngredient ingredient in currentRecipeLevel.ingredient)
        {
            itemCode = ingredient.itemProfile.itemCode;
            itemCount = ingredient.itemCount;
            this.inventory.DeductItem(itemCode, itemCount);
        }
    }
}
