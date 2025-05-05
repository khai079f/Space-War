using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShootRocket : AbilityNormalShoot
{
    [SerializeField] protected AbilityEnemyCtrlAbstract abilityEnemyCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityCtrl();
    }
    protected virtual void LoadAbilityCtrl()
    {
        if (this.abilityEnemyCtrl != null) return;
        this.abilityEnemyCtrl = transform.parent.GetComponent<AbilityEnemyCtrlAbstract>();
        Debug.LogWarning(transform.name + ": LoadAbilityCtrl for Enemy", gameObject);
    }
    protected override void SetShooter()
    {
        this.Shooter = this.abilityEnemyCtrl.BaseEnemyCtrl;
    }
    protected override string GetNameAbility()
    {
        return this.abilityBullet = "FireRocket";
    }
    protected override string GetNameBullet()
    {
        return this.nameBullet = BulletSpawner.bulletRoket;

    }
}
