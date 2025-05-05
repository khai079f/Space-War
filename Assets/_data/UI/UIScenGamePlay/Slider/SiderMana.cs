using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiderMana : SliderStatus
{
    protected static SiderMana instance;
    public static SiderMana Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (SiderMana.instance != null) Debug.LogWarning("Only 1 SiderMana allow to exist");
        SiderMana.instance = this;
    }
    protected override void ValueShowing()
    {
        this.UpdateManaShip();
        base.ValueShowing();
    }
    protected virtual void UpdateManaShip()
    {
        if (PlayerCtrl.instance.CurrentShip == null) return;
        this.maxValue = PlayerCtrl.instance.CurrentShip.ShipAbilities.GetMaxMana();
        this.currentValue = PlayerCtrl.instance.CurrentShip.ShipAbilities.GetCurrentMana();
    }
}
