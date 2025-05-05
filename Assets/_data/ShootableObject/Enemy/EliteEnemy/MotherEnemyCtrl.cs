using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherEnemyCtrl : BaseEnemyCtrl
{
    public AbilityMotherEnemy AbilitymotherEnemyCtrl => AbilityCtrl as AbilityMotherEnemy;
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
        return new MotherShipMoveState(this, stateEnemy.stateMachine);
    }
}

