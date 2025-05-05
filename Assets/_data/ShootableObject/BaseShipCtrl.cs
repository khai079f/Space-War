using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShipCtrl : GameMonoBehaviour
{
    [SerializeField] protected Despawn despawn;
    public Despawn Despawn => despawn;

    [SerializeField] protected DameReceiver dameReceiver;
    public DameReceiver DameReceiver => dameReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawn();
        this.LoadDameReceiver();
    }

    protected virtual void LoadDespawn()
    {
        if (this.despawn != null) return;
        this.despawn = transform.GetComponentInChildren<Despawn>();
        Debug.Log(transform.name + ": LoadDespawn", gameObject);
    }

    protected virtual void LoadDameReceiver()
    {
        if (this.dameReceiver != null) return;
        this.dameReceiver = transform.GetComponentInChildren<DameReceiver>();
        Debug.Log(transform.name + ": LoadDameReceiver", gameObject);
    }

    public abstract string WhatType();
}
