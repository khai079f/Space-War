using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityObjectCtrl : BaseEnemyCtrl
{
    [Header("Ability Object")]
    [SerializeField] protected SpawnPointsCtrl spawnPoint;
    public SpawnPointsCtrl SpawnPoint => spawnPoint;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPoint();
    }

    protected virtual void LoadSpawnPoint()
    {
        if (this.spawnPoint != null) return;
        this.spawnPoint = GetComponentInChildren<SpawnPointsCtrl>();
        Debug.Log(transform.name + ": LoadSpawnPoint", gameObject);
    }
}

