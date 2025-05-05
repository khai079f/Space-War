using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDespawn : DespawnByTime
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrlForShooter();
    }
    protected virtual void LoadCtrlForShooter()
    {
        if (bulletCtrl != null) return;
        this.bulletCtrl = transform.GetComponentInParent<BulletCtrl>();
        Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }
    public override void DeSpawnObj()
    {
        if (BulletSpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        BulletSpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
    public override void DeSpawnObj(BaseShipCtrl baseShipCtrl)
    {
        if (BulletSpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        BulletSpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
    protected override void WhatFX()
    {
        Transform FXRocketImpact = FXSpawner.Instance.SpawnBombExplosion(transform.parent.position, transform.parent.rotation,this.bulletCtrl.Shooter,this.bulletCtrl.AbilityData);
        FXRocketImpact.gameObject.SetActive(true);
    }
}
