using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TacticBaseState 
{
    protected TacticCtrl tacticCtrl;
    protected TacticStateMachine stateMachine;
    protected AircraftManagement aircraftManagement;
    public TacticBaseState(TacticCtrl tacticCtrl, TacticStateMachine stateMachine, AircraftManagement aircraftManagement)
    {
        this.tacticCtrl = tacticCtrl;
        this.stateMachine = stateMachine;
        this.aircraftManagement = aircraftManagement;
    }

    public virtual void Enter()
    {

        if (this.stateMachine != null) return;
        this.stateMachine = tacticCtrl.StateTactic.stateMachine;
    }
    public abstract void Execute();
    public abstract void Exit();

    protected virtual void Dissolution()
    {
        TacticBaseState newState = new TacticStateDissolution(this.tacticCtrl, this.stateMachine, this.aircraftManagement); // Tạo trạng thái MoveState

        this.stateMachine.ChangeState(newState); // Chuyển sang trạng thái MoveState

    }
}
