using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityReinforcedArmor : AbilityShipAbstract
{
    [SerializeField] protected float currentArmorIncrease = 2;
    protected virtual void IncreaseArmor()
    {
        this.currentArmorIncrease = this.abilityData.Dame * this.abilityData.Lv;
        this.shipPlayAbleAbilities.ShipCtrl.GetDameReceiver().IncreaseCurrentArmor(this.currentArmorIncrease);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        // Đăng ký hàm HandleLevelUp với event OnLevelUp
        this.abilityData.OnLevelUp += IncreaseArmor;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        // Hủy đăng ký hàm HandleLevelUp khi đối tượng bị vô hiệu hóa
        this.abilityData.OnLevelUp -= IncreaseArmor;
    }
}
