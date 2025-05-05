using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLookAtEnemy : LookAtTarget
{
    protected virtual void LoadPlayerPos(ShipCtrl player)
    {
        if (this.objectTarget != null) return;
        if (player == null) return;
        this.objectTarget = player.transform;
    }
    public virtual Transform GetPlayerPos()
    {
        if (this.objectTarget == null) return null;
        return this.objectTarget;
    }
    protected override Transform ParentObject()
    {
        return this.parentPos = transform.parent.parent;
    }
    protected override void Start()
    {
        base.Start();

        if (PlayerCtrl.instance == null) return;
        this.LoadPlayerPos(PlayerCtrl.instance.CurrentShip);
        PlayerCtrl.instance.shipSeletected += LoadPlayerPos;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerCtrl.instance.shipSeletected -= LoadPlayerPos;
    }
}
