using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnemyDameSender : DameSender
{
    [SerializeField] protected AbilityActivateSelfDestruct abilityEnemyCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityEnemyCtrl();
    }
    protected virtual void LoadAbilityEnemyCtrl()
    {
        if (abilityEnemyCtrl != null) return;
        this.abilityEnemyCtrl = (AbilityActivateSelfDestruct)transform.parent.GetComponent<BaseAbility>();
        Debug.Log(transform.name + ": LoadAbilityEnemyCtrl", gameObject);
    }
    public override void Send(DameReceiver dameReceiver)
    {
        base.Send(dameReceiver);
        this.DestroyAbility();
    }
    protected override void SetValueForAbility()
    {
        this.dame = this.abilityEnemyCtrl.AbilityData.Dame;
    }
    protected virtual void DestroyAbility()
    {
        this.abilityEnemyCtrl.AbilityEnemy.BaseEnemyCtrl.Despawn.DeSpawnObj(this.abilityEnemyCtrl.AbilityEnemy.BaseEnemyCtrl,abilityEnemyCtrl.AbilityData);
    }
}
