using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveCtrl : GameMonoBehaviour
{
    [SerializeField] protected DameSender dameSender;
    public DameSender DameSender { get => dameSender; }

    [SerializeField] protected Despawn despawn;
    public Despawn Despawn { get => despawn; }

    [SerializeField] protected BaseShipCtrl shooter;
    public BaseShipCtrl Shooter => shooter;
    [SerializeField] protected AbilityData abilityData;
    public AbilityData AbilityData => abilityData;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDameSender();
        this.LoadDespawnBullet();
    }

    protected virtual void LoadDameSender()
    {
        if (dameSender != null) return;
        this.dameSender = transform.GetComponentInChildren<DameSender>();
        Debug.Log(transform.name + ": LoadDameSender", gameObject);
    }

    protected virtual void LoadDespawnBullet()
    {
        if (despawn != null) return;
        this.despawn = transform.GetComponentInChildren<Despawn>();
        Debug.Log(transform.name + ": LoadDespawnBullet", gameObject);
    }

    public virtual void SetShotter(BaseShipCtrl shooter)
    {
        this.shooter = shooter;
    }
    public virtual void SetAbilityData(AbilityData abilityData)
    {
        this.abilityData = abilityData;
    }
}
