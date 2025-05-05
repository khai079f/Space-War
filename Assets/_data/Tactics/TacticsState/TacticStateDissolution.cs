using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticStateDissolution : TacticBaseState
{

    public TacticStateDissolution(TacticCtrl tacticCtrl, TacticStateMachine stateMachine, AircraftManagement aircraftManagement) : base(tacticCtrl, stateMachine, aircraftManagement)
    {
    }
    public override void Enter()
    {
        base.Enter();
       // this.aircraftManagement.DissoluTactic();
      //  Debug.Log(stateMachine.CurrentState);
        this.tacticCtrl.Despawn.DeSpawnObj(this.tacticCtrl);
    }
    public override void Execute()
    {

        
    }

    public override void Exit()
    {
       
    }
}
