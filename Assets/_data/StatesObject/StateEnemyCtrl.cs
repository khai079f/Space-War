using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyCtrl 
{
    protected BaseState EnemyState;
    public StateMachine stateMachine;
    public virtual void InitializeStateMachine(BaseState baseState)
    {
        this.stateMachine = new StateMachine();
        this.EnemyState = baseState;
        this.stateMachine.Initialize(EnemyState);

    }
}
