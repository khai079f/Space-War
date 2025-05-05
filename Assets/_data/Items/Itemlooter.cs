using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Itemlooter : InventoryAbstract
{
    [Header("Item looter")]
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] protected ShipCtrl shipCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadRigidbody();
        this.LoadShipCtrl();
    }
    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<SphereCollider>();
        this._collider.isTrigger = true;
        this._collider.radius = 0.365f;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody>();
        this._rigidbody.isKinematic = true;
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }
    protected virtual void LoadShipCtrl()
    {
        if (this.shipCtrl != null) return;
        this.shipCtrl = GetComponentInParent<ShipCtrl>();
        Debug.Log(transform.name + ": LoadShipCtrl", gameObject);
    }

    protected virtual void OnTriggerEnter(Collider collider)
    {

        LootingItem(collider);
    }

    protected virtual void LootingItem(Collider collider)
    {
        ItemPickupable itemPickupable = collider.GetComponent<ItemPickupable>();
        if (itemPickupable == null) return;
        ItemCode itemCode = itemPickupable.GetItemCode();
        //int count = ItemDropSpawner.Instance.countDropRate;
        ItemInventory itemInventory = itemPickupable.ItemCtrl.ItemInventory;
        itemPickupable.Picked();
        this.shipCtrl.LevelSystem.GainXP(itemInventory.itemProfile.defaultMaxStack);
      //  Debug.Log(itemPickupable.ItemCtrl.ItemInventory.itemId);
      //  Debug.Log(itemPickupable.ItemCtrl.ItemInventory.itemProfile.itemName);

    }


}
