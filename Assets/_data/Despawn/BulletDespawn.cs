using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawn : DespawnByTime
{
    public override void DeSpawnObj()
    {
        if (BulletSpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        BulletSpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
    
}
