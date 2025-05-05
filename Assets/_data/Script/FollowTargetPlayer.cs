using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetPlayer : FollowTarget
{
    protected override void OnEnable()
    {
        PlayerCtrl.instance.shipSeletected += OnCurrentShipUpdated;
    }

    protected override void OnDisable()
    {
        PlayerCtrl.instance.shipSeletected -= OnCurrentShipUpdated;
    }
    private void OnCurrentShipUpdated(ShipCtrl shipSelected)
    {
        // Thực hiện hành động khi currentShip được cập nhật
        this.SetTarget(shipSelected.transform);
    }
}
