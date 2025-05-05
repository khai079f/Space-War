using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDestruct : DespawnByHp
{

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    public override void DeSpawnObj()
    {
        if (EnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);

        EnemySpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();
    }
    public override void DeSpawnObj(BaseShipCtrl baseShipCtrl)
    {
        if (EnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        EnemySpawner.Instance.Despawn(transform.parent);
        base.DeSpawnObj();

    }
        public override void DeSpawnObj(BaseShipCtrl baseShipCtrl,AbilityData abilityData)
    {
        if (EnemySpawner.Instance == null) Debug.LogError(transform.name + ":Instance null", gameObject);
        EnemySpawner.Instance.Despawn(transform.parent);
        this.UseFXWithAbilityData(abilityData);

    }
    protected virtual void UseFXWithAbilityData(AbilityData abilityData){
        Transform FXRocketImpact = FXSpawner.Instance.SpawnBombExplosion(transform.parent.position, transform.parent.rotation, this.baseShipCtrl, abilityData);
        FXRocketImpact.gameObject.SetActive(true);
    }
    protected override void WhatFX()
    {
        Transform FXRocketImpact = FXSpawner.Instance.Spawn(FXSpawner.Explosion,transform.parent.position, transform.parent.rotation);
        FXRocketImpact.gameObject.SetActive(true);
    }
}
