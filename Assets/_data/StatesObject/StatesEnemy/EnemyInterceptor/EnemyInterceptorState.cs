using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInterceptorState : StateEnemyCtrl
{
/*    [SerializeField] protected EnemyInterceptorCtrl enemyInterceptorCtrl;
    protected override void LoadBaseShipCtrl()
    {
        if (this.enemyInterceptorCtrl != null) return;
        this.enemyInterceptorCtrl = transform.GetComponentInParent<EnemyInterceptorCtrl>();
        Debug.LogWarning(transform.name + ":LoadBaseShipCtrl", gameObject);
    }
    public override void InitializeStateMachine(BaseState baseState)
    {
        this.stateMachine = new StateMachine();
        this.EnemyState = new EnemyInterceptorMoveState(this.enemyInterceptorCtrl, this.stateMachine);
        this.stateMachine.Initialize(EnemyState);

    }*/

}
