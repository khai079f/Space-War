using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByHp : Despawn
{
    [SerializeField] protected BaseShipCtrl baseShipCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseShipCtrl();
    }
    protected virtual void LoadBaseShipCtrl()
    {
        if (this.baseShipCtrl != null) return;
        this.baseShipCtrl = transform.GetComponentInParent<BaseShipCtrl>();
        Debug.Log(transform.parent.name+ ":LoadBaseShipCtrl",gameObject);
    }
    protected override bool CanDespawn()
    {
        return this.baseShipCtrl.DameReceiver.IsDead();

    }

    protected override void WhatFX()
    {
        //
    }


}
