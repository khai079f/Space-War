using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiverlightEnemyCtrl : BaseEnemyCtrl
{
    public AbilitySilverlightEnemy abilitySiverlightEnemyCtrl => AbilityCtrl as AbilitySilverlightEnemy;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadComponentInChildren(ref this.abilityCtrl, "MotherEnemyCtrl");

    }
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
        return new SilverlightMoveState(this, stateEnemy.stateMachine);
    }
}
