using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDespawn : DespawnByHp
{

    public override void DeSpawnObj()
    {
        if (EnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        EnemySpawner.Instance.Despawn(transform.parent);
        this.transform.parent.SetParent(EnemySpawner.Instance.Holder);
        base.DeSpawnObj();
    }
    public override void DeSpawnObj(BaseShipCtrl baseShipCtrl)
    {
        if (EnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        EnemySpawner.Instance.Despawn(transform.parent);
        this.transform.parent.SetParent(EnemySpawner.Instance.Holder);
        base.DeSpawnObj();
    }

}