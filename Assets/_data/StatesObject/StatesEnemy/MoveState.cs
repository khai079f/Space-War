using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    protected CheckDistanceLevel.LevelForDistance LevelForDistance;
    protected Transform playerPos;
    public MoveState(BaseEnemyCtrl baseEnemy, StateMachine stateMachine) : base(baseEnemy, stateMachine)
    {
        this.baseEnemy = baseEnemy;
    }
    public override void Enter()
    {
        base.Enter();
        //  Debug.Log("Enter MoveState");
        this.baseEnemy.AbilityCtrl.MoveFoward.ResumeMovement();
    }

    public override void Execute()
    {
        // Debug.Log("MoveState");
        this.LookAtPlayer();
        this.GetLevelForDistance();

    }
    public override void Exit()
    {
        // Bất kỳ logic nào cần khi rời trạng thái
    }
    protected virtual void GetLevelForDistance()
    {
        this.playerPos = this.baseEnemy.AbilityCtrl.LookAtEnemy.GetPlayerPos();
        if (this.playerPos == null) return;
        this.LevelForDistance = this.baseEnemy.AbilityCtrl.DistanceLevel.GetDistanceLevel(this.baseEnemy.transform, this.playerPos);
    }


    protected virtual void LookAtPlayer()
    {
        Transform playerPos = this.baseEnemy.AbilityCtrl.LookAtEnemy.GetPlayerPos();
    }

}