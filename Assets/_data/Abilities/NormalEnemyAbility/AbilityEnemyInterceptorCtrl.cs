using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityEnemyInterceptorCtrl : AbilityEnemyCtrlAbstract
{
    [Header("Ability Enemy Interceptor Ctrl")]
    [SerializeField] protected AbilityActivateSelfDestruct selfDestruct;
    public AbilityActivateSelfDestruct SelfDestruct => selfDestruct;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbility(ref this.selfDestruct, "AbilityActivateSelfDestruct");

    }
}
