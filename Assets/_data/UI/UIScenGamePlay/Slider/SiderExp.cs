using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiderExp : SliderStatus
{
    protected static SiderExp instance;
    public static SiderExp Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (SiderExp.instance != null) Debug.LogWarning("Only 1 SiderExp allow to exist");
        SiderExp.instance = this;
    }
    protected override void ValueShowing()
    {
        this.UpdateExp();
        base.ValueShowing();
    }
    protected virtual void UpdateExp()
    {
        if (PlayerCtrl.instance.CurrentShip == null) return;
        this.maxValue = PlayerCtrl.instance.CurrentShip.LevelSystem.GetEXPNextLevel();
        this.currentValue = PlayerCtrl.instance.CurrentShip.LevelSystem.GetcurrentXP();
    }
}
