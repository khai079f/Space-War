using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaEnemyCtrl : BaseEnemyCtrl
{
    public AbilityPlasmaEnemy AbilityPlasmaEnemy => AbilityCtrl as AbilityPlasmaEnemy;
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
        return new PlasmaEnemyMoveState(this, stateEnemy.stateMachine);
    }
}
