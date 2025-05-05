using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyDespawnByHp : DespawnByHp
{
    public override void DeSpawnObj()
    {
        if (BossEnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        BossEnemySpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
        UIEndGame.Instance.Toggle();
    }
    public override void DeSpawnObj(BaseShipCtrl baseShipCtrl)
    {
        if (BossEnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        BossEnemySpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
        UIEndGame.Instance.Toggle();
    }
}
