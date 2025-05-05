using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitiesShipImpact : ImpactAbstract
{
    [SerializeField] protected AbilitylethalityShipPlayer abilityShip;
    [SerializeField] protected  Collider2D colliderShip;
    protected override string GetTypeShooter()
    {
        if (this.abilityShip.ShipPlayAbleAbilities.ShipCtrl == null) return null;
        this.baseShooter = this.abilityShip.ShipPlayAbleAbilities.ShipCtrl;
        if (this.baseShooter == null) return null;
        return this.shooterType = this.baseShooter.WhatType();
    }

    protected override void LoadCollider()
    {
        if (colliderShip != null) return;
        colliderShip = GetComponent<Collider2D>();
        colliderShip.isTrigger = true;
        Debug.LogWarning(transform.name + ":LoadCollider", gameObject);
    }

    protected override void LoadCtrlForShooter()
    {
        if (abilityShip != null) return;
        abilityShip = GetComponentInParent<AbilitylethalityShipPlayer>();
        Debug.LogWarning(transform.name + ":LoadCtrlForShooter", gameObject);
    }
}
