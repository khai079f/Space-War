using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState 
{
    protected BaseEnemyCtrl baseEnemy;
    protected StateMachine stateMachine;
    public BaseState(BaseEnemyCtrl baseEnemy, StateMachine stateMachine)
    {
        this.baseEnemy = baseEnemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    // if (this.stateMachine != null) Debug.Log(this.stateMachine.CurrentState);
        
        if (this.stateMachine != null) return;
        this.stateMachine = baseEnemy.StateEnemy.stateMachine;

    }
    public abstract void Execute();
    public abstract void Exit();
}
