using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySilverlightEnemy : AbilityEnemyCtrlAbstract
{
    [Header("Ability Silverlight Enemy")]
    [SerializeField] protected AbilityManangerMachinGun manangerMachinGun;
    public AbilityManangerMachinGun ManangerMachinGun => manangerMachinGun;
    [SerializeField] protected AbilityMissileMananger missileMananger;
    public AbilityMissileMananger MissileMananger => missileMananger;
    [SerializeField] protected AbilityOverdriveStrike abilityOverdriveStrike;
    public AbilityOverdriveStrike AbilityOverdriveStrike => abilityOverdriveStrike;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbility(ref this.manangerMachinGun, "AbilityManangerMachinGun");
        this.LoadAbility(ref this.missileMananger, "AbilityMissileMananger");
        this.LoadAbility(ref this.abilityOverdriveStrike, "AbilityOverdriveStrike");
    }

}
