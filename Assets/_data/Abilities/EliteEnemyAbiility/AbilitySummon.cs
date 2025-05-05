using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySummon : AbilityEnemyAbstract
{
    [Header("Ability Summon")]
    [SerializeField] protected Spawner spawner;
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
    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        this.Summoning();
    }

    protected virtual void Summoning()
    {
        if (!this.isReady) return;
        this.Summon();
    }
    protected virtual Transform Summon()
    {
        Transform spawnPos = this.spawnPoint.GetRandom();

        Transform minionPrefab = this.spawner.GetPrefabByName("EnemyInterceptor");
        Transform minion = this.spawner.Spawn(minionPrefab.name, spawnPos.position, spawnPos.rotation);
        minion.gameObject.SetActive(true);
        this.Active();
        return minion;
    }

    public override void UseAbilitySkill()
    {
        //
    }

}
