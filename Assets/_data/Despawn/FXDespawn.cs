using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDespawn : DespawnByTime
{
    public override void DeSpawnObj()
    {
        if (FXSpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        FXSpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
    public override void DeSpawnObj(BaseShipCtrl baseShipCtrl)
    {
        if (FXSpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        FXSpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
}
