using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipMoveState : MoveState
{
    protected MotherEnemyCtrl motherEnemy;

    public MotherShipMoveState(MotherEnemyCtrl motherEnemy, StateMachine stateMachine) : base(motherEnemy, stateMachine)
    {
        this.motherEnemy = motherEnemy;
    }
    public override void Enter()
    {
        base.Enter();
        this.baseEnemy.AbilityCtrl.LookAtEnemy.gameObject.SetActive(false);
        this.baseEnemy.AbilityCtrl.MoveFoward.gameObject.SetActive(false);
    }
    public override void Execute()
    {
        // Debug.Log("MoveState");
        //
    }
}
