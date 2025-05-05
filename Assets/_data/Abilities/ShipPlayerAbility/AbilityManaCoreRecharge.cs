using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManaCoreRecharge : AbilityShipAbstract
{
    [SerializeField] protected float currentManaIncrease = 0;
    protected virtual void IncreaseMana()
    {
        this.currentManaIncrease = this.abilityData.Dame ;
        this.shipPlayAbleAbilities.ShipCtrl.ShipProfileSO.UpdateManaRecovery(this.currentManaIncrease);
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
