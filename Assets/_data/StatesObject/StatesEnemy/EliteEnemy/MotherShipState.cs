using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipState : StateEnemyCtrl
{
/*    [SerializeField] protected MotherEnemyCtrl motherEnemyCtrl;
    protected override void LoadBaseShipCtrl()
    {
        if (this.motherEnemyCtrl != null) return;
        this.motherEnemyCtrl = transform.GetComponentInParent<MotherEnemyCtrl>();
        Debug.LogWarning(transform.name + ":LoadBaseShipCtrl", gameObject);
    }
    public override void InitializeStateMachine(BaseState baseState)
    {
        this.stateMachine = new StateMachine();
        this.EnemyState = new MotherShipMoveState(this.motherEnemyCtrl, this.stateMachine);
        this.stateMachine.Initialize(EnemyState);

    }*/
}
