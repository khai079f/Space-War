using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDurableHull : AbilityShipAbstract
{
    [SerializeField] protected float currentHpIncrease =2;
    protected virtual void IncreaseHp()
    {
        this.currentHpIncrease = this.abilityData.Dame * this.abilityData.Lv;
        this.shipPlayAbleAbilities.ShipCtrl.DameReceiver.IncreaseCurrentHp(this.currentHpIncrease);
        this.shipPlayAbleAbilities.ShipCtrl.DameReceiver.IncreaseCurrentMaxHp(this.currentHpIncrease);
        SiderHPlayerShip.Instance.UpdateCurrentValue(this.shipPlayAbleAbilities.ShipCtrl.DameReceiver.GetCurrentHp());
        SiderHPlayerShip.Instance.UpdateMaxValue(this.shipPlayAbleAbilities.ShipCtrl.DameReceiver.GetMaxHp());
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        // Đăng ký hàm HandleLevelUp với event OnLevelUp
        this.abilityData.OnLevelUp += IncreaseHp;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        // Hủy đăng ký hàm HandleLevelUp khi đối tượng bị vô hiệu hóa
        this.abilityData.OnLevelUp -= IncreaseHp;
    }
}
