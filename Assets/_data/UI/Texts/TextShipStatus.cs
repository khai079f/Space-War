using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextShipStatus : BaseText
{
    protected virtual void FixedUpdate()
    {
        this.UpdateHpShip();
    }
    protected virtual void UpdateHpShip()
    {
        if (PlayerCtrl.instance.CurrentShip == null) return;

    }
}
