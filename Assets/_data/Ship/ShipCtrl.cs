using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCtrl : BaseShipCtrl
{
    [Header("ShipCtrl")]
    [SerializeField] protected ShipProfileSO shipProfileSO;
    public ShipProfileSO ShipProfileSO => shipProfileSO;
    [SerializeField] protected ShipModelCtrl shipModelCtrl;
    public ShipModelCtrl ShipModelCtrl => shipModelCtrl;
    [SerializeField] protected ShipMoveKeyboard shipMoveKeyboard;
    public ShipMoveKeyboard ShipMoveKeyboard => shipMoveKeyboard;
    [SerializeField] protected Itemlooter itemlooder;
    public Itemlooter Itemlooter { get => itemlooder; }
    [SerializeField] protected ShipPlayAbleAbilities shipAbilities;
    public ShipPlayAbleAbilities ShipAbilities => shipAbilities;
    [SerializeField] protected LevelSystem levelSystem;
    public LevelSystem LevelSystem => levelSystem;
    protected ShipObjDameReceiver ShipPlayerReceiver;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipProfile();
        this.LoadComponentInChildren(ref this.itemlooder, "Inventory");
        this.LoadComponentInChildren(ref this.shipModelCtrl, "ShipModelCtrl");
        this.LoadComponentInChildren(ref this.shipMoveKeyboard, "ShipMoveKeyboard");
        this.LoadComponentInChildren(ref this.shipAbilities, "ShipPlayAbleAbilities");
        this.LoadComponentInChildren(ref this.levelSystem, "LevelSystem");
    }
    protected virtual void LoadShipProfile()
    {
        if (this.ShipProfileSO != null) return;
        string resPath = "ShootableObject/Ship/ShipProfile/" + transform.name;
        this.shipProfileSO = Resources.Load<ShipProfileSO>(resPath);
        Debug.Log(transform.name + ": ShipProfile", gameObject);
    }
    protected virtual void LoadComponentInChildren<T>(ref T component, string componentName) where T : Component
    {
        if (component != null) return;
        component = transform.GetComponentInChildren<T>();
        Debug.LogWarning(transform.name + ": Loaded " + componentName, gameObject);
    }

    public virtual ShipObjDameReceiver GetDameReceiver()
    {
        if (this.dameReceiver == null) Debug.LogError("Lỗi null ShipObjDameReceiver");
        ShipObjDameReceiver ShipPlayerReceiver = dameReceiver as ShipObjDameReceiver;
        return ShipPlayerReceiver;
    }
    public override string WhatType()
    {
        return this.ShipProfileSO.TypeObject.ToString();
    }
}

