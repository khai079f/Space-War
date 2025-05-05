using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticState : BaseState
{
    protected NormalEnemyCtrl normalEnemy;
    public TacticState(NormalEnemyCtrl baseEnemy, StateMachine stateMachine) : base(baseEnemy, stateMachine)
    {
        this.normalEnemy = baseEnemy;
    }
    public override void Enter()
    {
        base.Enter();
        this.baseEnemy.AbilityCtrl.LookAtEnemy.SetStateLook(false);
    }
    public override void Execute()
    {
    }

    public override void Exit()
    {
       
    }

}
