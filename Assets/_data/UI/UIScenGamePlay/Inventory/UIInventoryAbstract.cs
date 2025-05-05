using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIInventoryAbstract : GameMonoBehaviour
{
    [Header("Inventory Abstract")]
    [SerializeField] protected UIInventoryCtrl inventoryCtrl;
    public UIInventoryCtrl InventoryCtrl => inventoryCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIInventoryCtrl();

    }

    protected virtual void LoadUIInventoryCtrl()
    {
        if (inventoryCtrl != null) return;
        this.inventoryCtrl = transform.parent.GetComponent< UIInventoryCtrl>();
        Debug.LogWarning(transform.name + ": LoadContent ", gameObject);
    }

}