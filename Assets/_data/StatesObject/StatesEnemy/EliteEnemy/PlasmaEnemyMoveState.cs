using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaEnemyMoveState : MoveState
{
    protected PlasmaEnemyCtrl plasmaEnemyCtrl;
    protected bool usingAbility = false;
    public PlasmaEnemyMoveState(PlasmaEnemyCtrl plasmaEnemyCtrl, StateMachine stateMachine) : base(plasmaEnemyCtrl, stateMachine)
    {
        this.plasmaEnemyCtrl = plasmaEnemyCtrl;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Execute()
    {
        base.Execute();

        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Far)
        {
            this.baseEnemy.AbilityCtrl.MoveFoward.ResumeMovement();
            this.baseEnemy.AbilityCtrl.MoveFoward.MoveShip();
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Safe )
        { 
            this.baseEnemy.AbilityCtrl.MoveFoward.StopMovement();
            this.usingAbility = true;
            if (!this.plasmaEnemyCtrl.AbilityPlasmaEnemy.EnemyAttackLazer.IsReady) this.usingAbility = false;

        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.canAtk || this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Near)
        {
            this.baseEnemy.AbilityCtrl.MoveFoward.ResumeMovement();
            this.baseEnemy.AbilityCtrl.MoveFoward.MoveShipBackward();
        }
        if(this.usingAbility) this.plasmaEnemyCtrl.AbilityPlasmaEnemy.EnemyAttackLazer.ShootingLazer();


        // Debug.Log("MoveState");
        //
    }
}
