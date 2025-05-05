using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyCtrl : BaseEnemyCtrl
{
    public AbilityNormalEnemyCtrl AbilityNormalEnemyCtrl => abilityCtrl as AbilityNormalEnemyCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponentInChildren(ref this.abilityCtrl, "AbilityNormalEnemyCtrl");
    }

    protected override string GetObjTypeString()
    {
        return ObjectType.Enemy.ToString();
    }

    public override string WhatType()
    {
        return this.enemySO.ObjType.ToString();
    }

    // Khởi tạo state ban đầu cho enemy
    public override BaseState GetState()
    {
        return new NormalEnemyMoveState(this, this.stateEnemy.stateMachine);
    }
}
