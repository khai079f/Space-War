using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDespawn : DespawnByTime
{
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
        Transform FXRocketImpact = FXSpawner.Instance.Spawn(FXSpawner.Explosion, transform.parent.position, transform.parent.rotation);
        FXRocketImpact.gameObject.SetActive(true);
    }
}
