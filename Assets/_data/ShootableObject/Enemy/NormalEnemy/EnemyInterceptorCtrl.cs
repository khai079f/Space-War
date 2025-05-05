using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterceptorCtrl : BaseEnemyCtrl
{
    public AbilityEnemyInterceptorCtrl abilitAbilityEnemyInterceptory => AbilityCtrl as AbilityEnemyInterceptorCtrl;
    protected override string GetObjTypeString()
    {
        return ObjectType.Enemy.ToString();
    }

    public override string WhatType()
    {
        return this.enemySO.ObjType.ToString();
    }
    public override BaseState GetState()
    {
        return new EnemyInterceptorMoveState(this, stateEnemy.stateMachine);
    }
}

