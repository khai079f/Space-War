using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryAbstract : GameMonoBehaviour
{
    [Header("Inventory Abstract")]
    [SerializeField] protected Inventory inventory;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventory();

    }
    protected virtual void LoadInventory()
    {
        if (this.inventory != null) return;
        this.inventory = GetComponentInParent<Inventory>();
      //  Debug.Log(transform.name + ": LoadInventory", gameObject);
    }

}
