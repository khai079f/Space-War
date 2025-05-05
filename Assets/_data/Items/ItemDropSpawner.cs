using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : Spawner
{
    private static ItemDropSpawner instance;
    public static ItemDropSpawner Instance { get => instance; }

    private int _countDropRate;
    public int countDropRate => _countDropRate;

    protected override void Awake()
    {
        base.Awake();
        if (ItemDropSpawner.instance != null) Debug.LogWarning("Only 1 ItemDropSpawner allow to exist");
        ItemDropSpawner.instance = this;
    }

    public virtual void Drop(List<DropRate> dropRates,Vector3 pos, Quaternion rot)
    {
        // Todo: drop with rating
        if (dropRates.Count < 1) return;
        DropRate dropRate = GetDropRates(dropRates);
        ItemName itemName = dropRate.itemSO.itemName;
        rot.z = 0f;
        Transform itemDrop = this.Spawn(itemName.ToString(), pos, rot);
        if (itemDrop == null) return;
        itemDrop.gameObject.SetActive(true);
        GetCountItem(dropRate);

    }
    public virtual Transform DropByInventory(ItemInventory itemInventory, Vector3 pos, Quaternion rot)
    {
        ItemCode itemCode = itemInventory.itemProfile.itemCode;
        rot.z = 0f;
        Transform itemDrop = this.Spawn(itemCode.ToString(), pos, rot);
        if (itemDrop == null) return null;
        itemDrop.gameObject.SetActive(true);
        ItemCtrl itemCtrl = itemDrop.GetComponent<ItemCtrl>();
        itemCtrl.SetItemInventory(itemInventory);
        this._countDropRate = itemInventory.itemCount;
        return itemDrop;


    }

    protected virtual void GetCountItem(DropRate dropRateItem)
    {
        DropRate dropRate = dropRateItem;
        this._countDropRate = Random.Range(dropRate.minDrop, dropRate.maxDrop);
    }
    protected virtual DropRate GetDropRates(List<DropRate> dropRates)
    {
        // Tính tổng tỷ lệ
        float totalWeight = 0f;
        foreach (DropRate dropRate in dropRates)
        {
            totalWeight += dropRate.dropRate;
        }

        // Sinh ra một số ngẫu nhiên từ 0 đến tổng tỷ lệ
        float randomValue = Random.Range(0f, totalWeight);

        // Duyệt qua danh sách và chọn vật phẩm dựa trên giá trị ngẫu nhiên
        float cumulativeWeight = 0f;
        foreach (DropRate dropRate in dropRates)
        {
            cumulativeWeight += dropRate.dropRate;
            if (randomValue <= cumulativeWeight)
            {
                return dropRate;
            }
        }

        return null; // Trường hợp không chọn được item nào
    }
}
