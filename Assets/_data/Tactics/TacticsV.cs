using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsV : TacticCtrl
{
    public override void DoBehaviorTactic()
    {
      //
    }

    public override TacticBaseState GetStateForTactic()
    {
        return new TacticStateMove(this, this.stateTactic.stateMachine, this.aircraftManagement);
    }
    protected virtual TacticBaseState DissolutionSate()
    {
        return new TacticStateDissolution(this, this.stateTactic.stateMachine, this.aircraftManagement);
    }
    protected override void SetNameEnemyUnit()
    {
        if (this.nameEnemy.Count != 0) return;
        this.nameEnemy.Add(EnemySpawner.Instance.Prefabs[2].name);
    }
}
