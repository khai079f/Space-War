using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectrofieldLeveUp : LevelUpStrategy
{
    protected AbilityElectrofield abilityElectrofield;
    public ElectrofieldLeveUp(AbilityElectrofield abilityElectrofield) : base(abilityElectrofield)
    {
        this.abilityElectrofield = abilityElectrofield;
    }
    public override void LevelUp(AbilityData abilityData)
    {
        abilityData.IncreaseStatus(abilityData.contentLevelup.damageIncrease, abilityData.contentLevelup.cooldownReduction, abilityData.contentLevelup.manaUseReduction);
        this.abilityElectrofield.UpdateElectrofield(2,1);
    }
}
