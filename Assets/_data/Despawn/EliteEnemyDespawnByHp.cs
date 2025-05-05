using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyDespawnByHp : DespawnByHp
{
    public override void DeSpawnObj()
    {
        if (EliteEnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        EliteEnemySpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
    public override void DeSpawnObj(BaseShipCtrl baseShipCtrl)
    {
        if (EliteEnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        EliteEnemySpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
}
