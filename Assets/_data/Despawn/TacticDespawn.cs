using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticDespawn : Despawn
{
    public override void DeSpawnObj()
    {
        if (TacticSpawnerCtrl.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        TacticSpawnerCtrl.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
    public virtual void DeSpawnObj(TacticCtrl tacticCtrl)
    {
        if (TacticSpawnerCtrl.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        TacticSpawnerCtrl.Instance.Despawn(tacticCtrl.transform);
    }

    protected override bool CanDespawn()
    {
        return false;
    }

    protected override void WhatFX()
    {
       //
    }
}
