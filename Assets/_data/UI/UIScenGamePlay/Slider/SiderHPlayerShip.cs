using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiderHPlayerShip : SliderStatus
{
    protected static SiderHPlayerShip instance;
    public static SiderHPlayerShip Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (SiderHPlayerShip.instance != null) Debug.LogWarning("Only 1 SiderHPPlayerShip allow to exist");
        SiderHPlayerShip.instance = this;
    }
    protected override void ValueShowing()
    {
        this.UpdateHpShip();
        base.ValueShowing();
    }
    protected virtual void UpdateHpShip()
    {
        if (PlayerCtrl.instance.CurrentShip == null) return;
        this.maxValue = PlayerCtrl.instance.CurrentShip.DameReceiver.GetMaxHp();
        this.currentValue = PlayerCtrl.instance.CurrentShip.DameReceiver.GetCurrentHp();
    }
   
}
