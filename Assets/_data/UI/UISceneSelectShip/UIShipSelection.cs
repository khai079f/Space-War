using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShipSelection : UIShipSelectAbstract
{
    [Header("UI ShipSelection")]
    private static UIShipSelection instance;
    public static UIShipSelection Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (UIShipSelection.instance != null) Debug.LogError("Only 1 UIShipSelection allow to exist");
        UIShipSelection.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        ShowShip();
    }
    protected virtual void ShowShip()
    {
       // Debug.Log("shipName:" + ShipProfile.GetAllShipProfile()[0].shipName);
        List<ShipProfileSO> ships = ShipProfileSO.GetAllShipProfile();
        UIShipSpawner uIShipSpawner = this.shipSelectCtrl.UIShipSpawner;
        foreach(ShipProfileSO ship in ships)
        {
            uIShipSpawner.SpawnShip(ship);
        }
       
    }

    protected virtual void ClearItems()
    {
        this.shipSelectCtrl.UIShipSpawner.ClearItems();
    }
}
