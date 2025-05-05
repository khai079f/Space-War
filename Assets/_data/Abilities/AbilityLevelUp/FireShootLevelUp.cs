using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShootLevelUp : LevelUpStrategy
{
    protected BaseAbility shipShooting;
    public FireShootLevelUp(BaseAbility shipShooting) : base(shipShooting)
    {
        this.shipShooting = shipShooting;
    }
    public override void LevelUp(AbilityData abilityData)
    {
        abilityData.IncreaseStatus(0.5f, 0.02f, 0);
    }
}
