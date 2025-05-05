using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : Spawner
{
    protected static FXSpawner instance;
    public static FXSpawner Instance => instance;

    public static string smokeOne = "Smoke_1";

    public static string impactOne = "Impact_1";

    public static string Rocket_impact = "Rocket_impact";

    public static string dashOne = "Dash_1";

    public static string EnergyShield = "EnergyShield";
    public static string Explosion = "Explosion";
    public static string BombExplosion = "BombExplosion";
    [SerializeField] protected ShockwaveCtrl shockwaveCtrl;
    protected override void Awake()
    {
        base.Awake();
        if (FXSpawner.instance != null) Debug.LogWarning("Only 1 FXSpawner allow to exist");
        FXSpawner.instance = this;
    }
    protected override void LoadComponents()
    {
        this.LoadShockwaveCtrl();
        base.LoadComponents();

    }
    protected virtual void LoadShockwaveCtrl()
    {
        if (this.shockwaveCtrl != null) return;
        this.shockwaveCtrl = transform.GetComponentInChildren<ShockwaveCtrl>();
        Debug.Log(transform.name + ": LoadShockwaveCtrl", gameObject);
    }
    public virtual Transform SpawnBombExplosion( Vector3 spawnPos, Quaternion rotation, BaseShipCtrl shooter, AbilityData abilityData)
    {
        Transform newPrefab = this.GetObjectFromPool(this.shockwaveCtrl.transform);
        ShockwaveCtrl shockwave = newPrefab.GetComponent<ShockwaveCtrl>();
        shockwave.SetShotter(shooter);
        shockwave.SetAbilityData(abilityData);
        shockwave.transform.SetPositionAndRotation(spawnPos, rotation);
        shockwave.transform.SetParent(this.holder);
        this.spawnedCount++;
        return newPrefab;
    }

    }
