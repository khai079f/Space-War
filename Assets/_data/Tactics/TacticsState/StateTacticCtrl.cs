using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTacticCtrl 
{
    protected TacticBaseState tacticBaseState;
    public TacticStateMachine stateMachine;
    public virtual void InitializeStateMachine(TacticBaseState baseState)
    {
        this.stateMachine = new TacticStateMachine();
        this.tacticBaseState = baseState;
        this.stateMachine.Initialize(tacticBaseState);

    }
}
