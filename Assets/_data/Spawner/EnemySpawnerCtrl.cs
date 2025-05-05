using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerCtrl : SpawnerCtrl
{
    [Header("Enemy Spawner Ctrl")]
    [SerializeField] protected NormalEnemyCtrl normalEnemyCtrl;
    public NormalEnemyCtrl NormalEnemyCtrl => normalEnemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNormalEnemyCtrl();
    }

    protected virtual void LoadNormalEnemyCtrl()
    {
        if (this.normalEnemyCtrl != null) return;
        this.normalEnemyCtrl = transform.GetComponentInChildren<NormalEnemyCtrl>();
        if (this.normalEnemyCtrl == null) Debug.LogWarning("JunkSpawn object not found in the scene.");
        Debug.Log(spawner.name + ": LoadNormalEnemyCtrl ", gameObject);

    }
}
