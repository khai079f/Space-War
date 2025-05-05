using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManaCoreExpansion : AbilityShipAbstract
{
    [SerializeField] protected float currentManaIncrease = 2;
    protected virtual void IncreaseMana()
    {
        this.currentManaIncrease = this.abilityData.Dame * this.abilityData.Lv;
        this.shipPlayAbleAbilities.UpdateMaxMana(this.currentManaIncrease);
        this.shipPlayAbleAbilities.UpdateCurrentMana(this.currentManaIncrease);
        SiderMana.Instance.UpdateCurrentValue(this.shipPlayAbleAbilities.GetCurrentMana());
        SiderMana.Instance.UpdateMaxValue(this.shipPlayAbleAbilities.GetMaxMana());
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        // Đăng ký hàm HandleLevelUp với event OnLevelUp
        this.abilityData.OnLevelUp += IncreaseMana;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        // Hủy đăng ký hàm HandleLevelUp khi đối tượng bị vô hiệu hóa
        this.abilityData.OnLevelUp -= IncreaseMana;
    }
}
