using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterceptorMoveState : MoveState
{
    protected EnemyInterceptorCtrl EnemyInterceptor;

    public EnemyInterceptorMoveState(EnemyInterceptorCtrl normalEnemy, StateMachine stateMachine) : base(normalEnemy, stateMachine)
    {
        this.EnemyInterceptor = normalEnemy;
    }
    public override void Execute()
    {
        base.Execute();
        this.EnemyInterceptor.abilitAbilityEnemyInterceptory.MoveFoward.MoveShip();
        // Kiểm tra điều kiện tấn công
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.canAtk || this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Near)
        {
            this.EnemyInterceptor.abilitAbilityEnemyInterceptory.SelfDestruct.UseAbilitySkill();
        }

    }

    
}
