using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemyCtrl : NormalEnemyCtrl
{
    public override BaseState GetState()
    {
        return new BombEnemyMoveState(this, this.stateEnemy.stateMachine);
    }
}
