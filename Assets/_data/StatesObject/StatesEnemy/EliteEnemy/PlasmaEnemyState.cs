using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaEnemyState : StateEnemyCtrl
{
/*    [SerializeField] protected PlasmaEnemyCtrl plasmaEnemyCtrl;
    protected override void LoadBaseShipCtrl()
    {
        if (this.plasmaEnemyCtrl != null) return;
        this.plasmaEnemyCtrl = transform.GetComponentInParent<PlasmaEnemyCtrl>();
        Debug.LogWarning(transform.name + ":LoadBaseShipCtrl", gameObject);
    }
    public override void InitializeStateMachine(BaseState baseState)
    {
        this.stateMachine = new StateMachine();
        this.EnemyState = new PlasmaEnemyMoveState(this.plasmaEnemyCtrl, this.stateMachine);
        this.stateMachine.Initialize(EnemyState);

    }*/
}
