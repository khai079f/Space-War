using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticBombState : TacticState
{
    public TacticBombState(NormalEnemyCtrl baseEnemy, StateMachine stateMachine) : base(baseEnemy, stateMachine)
    {
        this.normalEnemy = baseEnemy;
    }
    public override void Enter()
    {
        base.Enter();
        this.baseEnemy.AbilityCtrl.MoveFoward.ResumeMovement();
        this.baseEnemy.AbilityCtrl.LookAtEnemy.SetStateLook(true);
    }
    public override void Execute()
    {
    }

    public override void Exit()
    {

    }

}
