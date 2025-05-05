using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : GameMonoBehaviour
{
    private static PlayerCtrl _instance;
    public static PlayerCtrl instance => _instance;
    [SerializeField] protected ShipCtrl currentShip;
    public ShipCtrl CurrentShip => currentShip;
    [SerializeField] protected AbilityGlobalPlayerCtrl abilityGlobalPlayer;
    public AbilityGlobalPlayerCtrl AbilityGlobalPlayer => abilityGlobalPlayer;

    public event Action<ShipCtrl> shipSeletected;
    protected override void Awake()
    {
        base.Awake();
        if (PlayerCtrl._instance != null)
        {
            Debug.LogError("Only one instance on scene");
            return;
        }
        PlayerCtrl._instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerAbility();

    }
    protected virtual void LoadPlayerAbility()
    {
        if (this.abilityGlobalPlayer != null) return;
        this.abilityGlobalPlayer = GetComponentInChildren<AbilityGlobalPlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerAbility", gameObject);
    }

    public virtual ShipCtrl GetCurrentShip()
    {
        if (this.currentShip != null) return null;
        this.currentShip = GetComponentInChildren<ShipCtrl>();
        if(this.currentShip == null) Debug.LogError(transform.name + ":Don't LoadCurrentShip", gameObject);
        shipSeletected?.Invoke(this.currentShip);
        return this.currentShip;

    }

}

