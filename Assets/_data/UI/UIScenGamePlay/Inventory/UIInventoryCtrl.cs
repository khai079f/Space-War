using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryCtrl : GameMonoBehaviour
{
    [SerializeField] protected Transform content;
    public Transform Content => content;
    [SerializeField] protected UIInventorySpawner inventorySpawner;
    public UIInventorySpawner InventorySpawner => inventorySpawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContent();
        this.LoadUIInventorySpawner();

    }

    protected virtual void LoadContent()
    {
        if (content != null) return;
        this.content = transform.Find("UIInventory").Find("Scroll View").Find("Viewport").Find("Content");
        Debug.LogWarning(transform.name + ": LoadContent ", gameObject);
    }
    protected virtual void LoadUIInventorySpawner()
    {
        if (inventorySpawner != null) return;
        this.inventorySpawner = GetComponentInChildren<UIInventorySpawner>();
        Debug.LogWarning(transform.name + ": LoadUIInventorySpawner ", gameObject);
    }
}