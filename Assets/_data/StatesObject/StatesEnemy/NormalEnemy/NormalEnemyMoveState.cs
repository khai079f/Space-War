using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyMoveState : MoveState
{
    protected NormalEnemyCtrl normalEnemy;
    public NormalEnemyMoveState(NormalEnemyCtrl normalEnemy, StateMachine stateMachine) : base(normalEnemy, stateMachine)
    {
        this.normalEnemy = normalEnemy;
        
    }
    public override void Enter()
    {
        base.Enter();
        this.normalEnemy.AbilityCtrl.LookAtEnemy.SetStateLook(true);
        this.normalEnemy.AbilityCtrl.LookAtEnemy.SetOffset(0);
    }
    public override void Execute()
    {
        base.Execute();
        this.normalEnemy.AbilityNormalEnemyCtrl.MoveFoward.MoveShip();
        this.BehaviorEnemy();
    }
    protected virtual void BehaviorEnemy()
    {
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Safe)
        {
            this.NormalAttack();
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.canAtk)
        {
            this.NormalAttack();
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Near)
        {
            this.ChangeState();
        }
    }
    public override void Exit()
    {
    }
    protected virtual void NormalAttack()
    {
        this.normalEnemy.AbilityNormalEnemyCtrl.Shootbullet.Shoot();

    }
    protected virtual void ChangeState()
    {
        stateMachine.ChangeState(this.normalEnemy.StateEnemyManager.RetreatState); // Chuyển sang trạng thái MoveState
    }
}
