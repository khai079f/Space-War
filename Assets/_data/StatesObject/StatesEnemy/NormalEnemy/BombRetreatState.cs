using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRetreatState : RetreatState
{
    public BombRetreatState(NormalEnemyCtrl normalEnemy, StateMachine stateMachine) : base(normalEnemy, stateMachine)
    {
        this.normalEnemy = normalEnemy;
        this.playerPos = normalEnemy.AbilityNormalEnemyCtrl.LookAtEnemy.GetPlayerPos();
        this.SetRotationRandom(); // Lấy giá trị ngẫu nhiên
    }
        public override void Execute()
    {
        this.normalEnemy.AbilityCtrl.MoveFoward.MoveShip();
        this.GetLevelForDistance();
        
        if (LevelForDistance == CheckDistanceLevel.LevelForDistance.Far) this.ChangeState();

    }
    protected override void ChangeState()
    {
        stateMachine.ChangeState(this.normalEnemy.StateEnemyManager.BombEnemyMoveState);
    }
    public override void Exit()
    {
        base.Exit();
        this.normalEnemy.AbilityCtrl.LookAtEnemy.SetStateLook(true);
    }
}
