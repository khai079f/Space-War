using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BaseState
{
    public DeadState(BaseEnemyCtrl baseEnemy, StateMachine stateMachine) : base(baseEnemy, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Execute()
    {
    }

    public override void Exit()
    {

    }

}
