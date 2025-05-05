using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class ItemPickupable : GameMonoBehaviour
{
    [SerializeField] protected ItemCtrl itemCtrl;
    public ItemCtrl ItemCtrl => itemCtrl;
    [SerializeField] protected SphereCollider _collider;

    public virtual void OnMouseDown()
    {
        Debug.Log(transform.parent.name);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadItemCtrl();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<SphereCollider>();
        this._collider.isTrigger = true;
        this._collider.radius = 0.19f;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }
    protected virtual void LoadItemCtrl()
    {
        if (this.itemCtrl != null) return;
        this.itemCtrl = transform.parent.GetComponent<ItemCtrl>();
        //Debug.Log(transform.name + ": LoadJunkCtrl", gameObject);
    }

    public static ItemCode String2Itemcode(string itemName)
    {
        return (ItemCode)System.Enum.Parse(typeof(ItemCode), itemName);
    }
    public virtual ItemCode GetItemCode()
    {
        return ItemPickupable.String2Itemcode(transform.parent.name);
    }
    public virtual void Picked()
    {
        this.ItemCtrl.DespawnItem.DeSpawnObj();
    }
}
