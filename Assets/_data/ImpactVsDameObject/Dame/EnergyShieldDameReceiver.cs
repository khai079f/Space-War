using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldDameReceiver : ShootableObjDameReceiver
{
    public override void Deduct(float deduct)
    {
        AbilityEnemyEnergyShield shield = CheckShield();
        float damededuct = 0;
        if (shield != null) damededuct = shield.ShieldTakeDamage(deduct);
        base.Deduct(damededuct);
        base.Deduct(deduct);
    }
    protected virtual AbilityEnemyEnergyShield CheckShield()
    {
        foreach (var ability in this.baseEnemyCtrl.AbilityCtrl.GetListEnemyAbilitys())
        {
            if (ability is AbilityEnemyEnergyShield shield)
            {
                return shield; // Trả về AbilityShipEnergyShield đầu tiên được tìm thấy
            }
        }
        return null; // Nếu không tìm thấy, trả về null
    }
}
