using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventorySpawner : Spawner
{
    protected static UIInventorySpawner instance;
    public static UIInventorySpawner Instance => instance;

    public static string normalItem = "UIIvnItem";

    [Header("UI Inventory Spawner")]
    [SerializeField] protected UIInventoryCtrl inventoryCtrl;
    public UIInventoryCtrl InventoryCtrl => inventoryCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (UIInventorySpawner.instance != null) Debug.LogWarning("Only 1 UIInventorySpawner allow to exist");
        UIInventorySpawner.instance = this;
    }
    protected override void LoadHolder()
    {
        this.LoadUIInventoryCtrl();
        if (this.holder != null) return;
        this.holder = this.inventoryCtrl.Content;
        Debug.Log(transform.name + ": LoadHolder", gameObject);

    }

    protected virtual void LoadUIInventoryCtrl()
    {
        if (this.inventoryCtrl != null) return;
        this.inventoryCtrl = transform.GetComponentInParent<UIInventoryCtrl>();
        Debug.Log(transform.name + ": LoadUIInventoryCtrl", gameObject);
    }

    public virtual void ClearItems()
    {
        foreach(Transform item in this.holder)
        {
            this.Despawn(item);
        }
    }

    public virtual void SpawnItem(ItemInventory item)
    {
        Transform uiItem = this.inventoryCtrl.InventorySpawner.Spawn(UIInventorySpawner.normalItem, Vector3.zero, Quaternion.identity);
        uiItem.transform.localScale = new Vector3(1, 1, 1);
        UIItemInventory itemInventory = uiItem.GetComponent<UIItemInventory>();
        itemInventory.ShowItem(item);
        uiItem.gameObject.SetActive(true);
    }
}
