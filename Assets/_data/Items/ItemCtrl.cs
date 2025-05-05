using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : GameMonoBehaviour
{
    [SerializeField] protected Transform model;
    public Transform Model { get => model; }

    [SerializeField] protected DespawnItem despawnItem;
    public DespawnItem DespawnItem { get => despawnItem; }

    [SerializeField] protected ItemInventory itemInventory;
    public ItemInventory ItemInventory { get => itemInventory; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadDespawnItem();
        this.LoadItemInventory();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.ResetValueItem();
    }
    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }
    protected virtual void LoadDespawnItem()
    {
        if (this.despawnItem != null) return;
        this.despawnItem = transform.GetComponentInChildren<DespawnItem>();
        Debug.Log(transform.name + ": LoadDespawnJunk", gameObject);
    }

    public virtual void SetItemInventory(ItemInventory itemInventory)
    {
        this.itemInventory = itemInventory.Clone();
    }

    protected virtual void LoadItemInventory()
    {
        if (this.itemInventory.itemProfile != null) return;
        ItemCode itemCode = ItemCodeParser.FromString(transform.name);
        ItemProfileSO itemProfile = ItemProfileSO.FindByItemCode(itemCode);
        this.itemInventory.itemProfile = itemProfile;
        ResetValueItem();
        Debug.Log(transform.name + ": LoadItemInventory", gameObject);
    }

    protected virtual void ResetValueItem() {
        this.itemInventory.itemCount = 1;
        this.itemInventory.upgradeLevel = 0;
    }
}
