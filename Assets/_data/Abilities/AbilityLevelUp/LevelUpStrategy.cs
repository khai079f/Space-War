using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelUpStrategy
{
    protected BaseAbility baseAbility;
    public LevelUpStrategy(BaseAbility baseAbility)
    {
        this.baseAbility = baseAbility;
    }
    public virtual void LevelUp(AbilityData abilityData)
    {
        abilityData.IncreaseStatus(abilityData.contentLevelup.damageIncrease, abilityData.contentLevelup.cooldownReduction, abilityData.contentLevelup.manaUseReduction);
    } 
}

