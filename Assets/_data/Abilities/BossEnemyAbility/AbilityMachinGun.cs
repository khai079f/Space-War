using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMachinGun : AbilityNormalShoot
{
    [Header("Ability MachinGun")]
    [SerializeField] protected AbilityManangerMachinGun manangerMachinGun;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityManangerMachinGun();
    }
    protected virtual void LoadAbilityManangerMachinGun()
    {
        if (this.manangerMachinGun != null) return ;
        this.manangerMachinGun = transform.GetComponentInParent<AbilityManangerMachinGun>();
        Debug.LogWarning(transform.name+ " LoadAbilityManangerMachinGun",gameObject);
    }
    protected override void LoadAbilityData()
    {
        if (this.abilityData != null) return;
        string resPath = "ShootableObject/Enemy/Ability/" + this.GetNameAbility();
        this.abilityData = Resources.Load<AbilityData>(resPath);
        Debug.LogWarning(transform.name + ": LoadAbilityData for Enemy", gameObject);
    }
    protected override void LoadPointShooter()
    {
        base.LoadPointShooter();
    }
    protected override void SetShooter()
    {
        if (this.manangerMachinGun == null) return;
        this.Shooter = this.manangerMachinGun.abilitySilverlight.BaseEnemyCtrl;

    }
    protected override string GetNameAbility()
    {
       return this.abilityBullet = "MachinGunShoot";
    }
}
