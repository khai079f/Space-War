using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class ImpactAbstract : GameMonoBehaviour
{
    [Header("Impact Abstract")]
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected BaseShipCtrl baseShooter;
    [SerializeField] protected string shooterType;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadRigidbody();
        this.LoadCtrlForShooter();
    }

    protected abstract void LoadCollider();

    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._rigidbody.isKinematic = true;
        Debug.Log(transform.name + ": LoadRigidbody", gameObject);
    }
    protected virtual bool CheckTypeShooter(Collider2D other)
    {
        this.GetTypeShooter();
        BaseShipCtrl baseShooter = other.GetComponentInParent<BaseShipCtrl>();
        if (baseShooter == null) { return false; }
       // Debug.Log("baseShooter.WhatType():"+ baseShooter.WhatType()+ "  shooterType:"+this.shooterType);
        if (baseShooter.WhatType() == this.shooterType) return true;
        return false;
    }
    protected virtual bool CheckColliderShip(Collider2D other)
    {
        BaseShipCtrl baseShooter = other.GetComponentInParent<BaseShipCtrl>();
        
        if (baseShooter == null) return false;
        return true;
    }
    public void SetColliderState(bool state)
    {
        gameObject.SetActive(state);
    }
    protected abstract void LoadCtrlForShooter();
    protected abstract string GetTypeShooter();


}
