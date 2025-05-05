using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverlightMoveState : MoveState
{
    protected SiverlightEnemyCtrl siverlightEnemyCtrl;
    protected bool usingAbility = false;

    public SilverlightMoveState(SiverlightEnemyCtrl siverlightEnemyCtrl, StateMachine stateMachine) : base(siverlightEnemyCtrl, stateMachine)
    {
        this.siverlightEnemyCtrl = siverlightEnemyCtrl;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Execute()
    {
        base.Execute();
        this.AiMove();
        this.AiUseOverdrive();
        this.Attack();


    }
    protected virtual void AiUseOverdrive()
    {
        if (this.siverlightEnemyCtrl.abilitySiverlightEnemyCtrl.AbilityOverdriveStrike.IsChargeEnergy)
        {
            this.baseEnemy.AbilityCtrl.LookAtEnemy.gameObject.SetActive(false);
        }
        else
        {
            this.baseEnemy.AbilityCtrl.LookAtEnemy.gameObject.SetActive(true);
        }
    }
    protected void AiMove()
    {
        this.baseEnemy.AbilityCtrl.MoveFoward.ResumeMovement();
        if (this.LevelForDistance != CheckDistanceLevel.LevelForDistance.Far && !this.siverlightEnemyCtrl.abilitySiverlightEnemyCtrl.AbilityOverdriveStrike.IsReady)
        {
            this.baseEnemy.AbilityCtrl.LookAtEnemy.SetOffset(90);
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Far)
        {
            this.baseEnemy.AbilityCtrl.LookAtEnemy.SetOffset(0);


        }
        this.baseEnemy.AbilityCtrl.MoveFoward.MoveShip();
    }
    protected virtual void Attack()
    {
        if(this.LevelForDistance != CheckDistanceLevel.LevelForDistance.Far)
        {
            this.siverlightEnemyCtrl.abilitySiverlightEnemyCtrl.ManangerMachinGun.AllGunFire();

        }
        this.siverlightEnemyCtrl.abilitySiverlightEnemyCtrl.MissileMananger.AllGunFire();
    }
}
