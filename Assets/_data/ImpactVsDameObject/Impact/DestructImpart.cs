using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DestructImpart : ImpactAbstract
{
    [SerializeField] protected BoxCollider2D boxCollider;
    [SerializeField] protected AbilityActivateSelfDestruct activateSelfDestruct;
    protected override string GetTypeShooter()
    {
        if (this.activateSelfDestruct.AbilityEnemy.BaseEnemyCtrl == null) return null;
        this.baseShooter = this.activateSelfDestruct.AbilityEnemy.BaseEnemyCtrl;
        if (this.baseShooter == null) return null;
        return this.shooterType = this.baseShooter.WhatType();
    }

    protected override void LoadCollider()
    {
        if (this.boxCollider != null) return;
        this.boxCollider = transform.GetComponent<BoxCollider2D>();
        Debug.LogWarning(transform.name + ":LoadCollider", gameObject);
    }

    protected override void LoadCtrlForShooter()
    {
        if (this.activateSelfDestruct != null) return;
        this.activateSelfDestruct = GetComponentInParent<AbilityActivateSelfDestruct>();
        Debug.LogWarning(transform.name + ":LoadCtrlForShooter", gameObject);
    }
    protected virtual bool IsShipPlayer(Collider2D other)
    {
        BaseShipCtrl baseShooter = other.GetComponentInParent<BaseShipCtrl>();
        if (baseShooter == null) { return false; }
        if (baseShooter.WhatType() == TypeObject.ShipPlayer.ToString()) return true;
        return false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.CheckTypeShooter(other)) return;
        if (!this.IsShipPlayer(other)) return;
        this.activateSelfDestruct.DameSender.Send(other.transform);
       // Debug.Log(other.transform.parent.name);
        // this.CreateImpactFX();

    }
}
