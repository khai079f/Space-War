using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : DespawnByDistance
{
    protected override void WhatFX()
    {
        Transform NewImpactFx = FXSpawner.Instance.Spawn(FXSpawner.impactOne, transform.parent.position, transform.parent.rotation);
        NewImpactFx.gameObject.SetActive(true);
    }
    public override void DeSpawnObj()
    {
        if (BulletSpawner.Instance == null) Debug.LogError(transform.name+ ":Instance null",gameObject);
        BulletSpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
    public override void DeSpawnObj(BaseShipCtrl baseShipCtrl)
    {
        if (BulletSpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        BulletSpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
}
