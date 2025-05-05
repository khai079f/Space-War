using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelectionData 
{
    private static ShipSelectionData instance;
    public static ShipSelectionData Instance
    {
        get
        {
            if (instance == null) instance = new ShipSelectionData();
            return instance;
        }
    }
    private ShipProfileSO selectedShip;
    public virtual void SetSelectedShip(ShipProfileSO selectedShip)
    {
        this.selectedShip = selectedShip;
    }
    public ShipProfileSO GetSelectedShip()
    {
        return this.selectedShip;
    }
    public ShipProfileSO GetDefaultShip()
    {
        this.selectedShip = ShipProfileSO.GetAllShipProfile()[1];
        return this.selectedShip ;
    }
}
