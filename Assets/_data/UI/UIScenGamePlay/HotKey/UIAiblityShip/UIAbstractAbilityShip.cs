using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIAbstractAbilityShip : GameMonoBehaviour
{
 
    [SerializeField] protected ShipCtrl CurrentShip;
    protected override void OnEnable()
    {
        PlayerCtrl.instance.shipSeletected += OnCurrentShipUpdated;
    }

    protected override void OnDisable()
    {
        PlayerCtrl.instance.shipSeletected -= OnCurrentShipUpdated;
    }
    private void OnCurrentShipUpdated(ShipCtrl newShip)
    {
        // Thực hiện hành động khi currentShip được cập nhật
        this.CurrentShip = newShip;
    }


}
