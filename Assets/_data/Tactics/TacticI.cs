using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticI : TacticCtrl
{
    public override TacticBaseState GetStateForTactic()
    {
        return new TacticStateBombMove(this, this.stateTactic.stateMachine, this.aircraftManagement);
    }
    protected override void SetNameEnemyUnit()
    {
        if (this.nameEnemy.Count != 0) return;
        this.nameEnemy.Add(EnemySpawner.Instance.Prefabs[3].name);
    }
    protected virtual void SetTargetForUnit()
    {
        if (this.aircraftManagement.CurrentUnitEnemys == null || this.aircraftManagement.CurrentUnitEnemys.Count == 0) return;

        Transform currentShip = PlayerCtrl.instance?.CurrentShip?.transform;

        UnitEnemyCtrl previousEnemy = null;

        foreach (var currentEnemy in this.aircraftManagement.CurrentUnitEnemys)
        {
            if (currentEnemy?.NomalEnemyCtrl == null)
            {
                previousEnemy = null; // Nếu phần tử không hợp lệ, bỏ qua và đặt previousEnemy là null
                continue;
            }

            var lookAtEnemy = currentEnemy.NomalEnemyCtrl.AbilityNormalEnemyCtrl.LookAtEnemy;
            var moveForward = currentEnemy.NomalEnemyCtrl.AbilityNormalEnemyCtrl.MoveFoward;

            // Bật tính năng "LookAtEnemy" và cho phép di chuyển
            lookAtEnemy.SetStateLook(true);

            if (previousEnemy == null)
            {
                // Nếu đây là phần tử đầu tiên hợp lệ
                if (currentShip != null)
                {
                    //Debug.Log(currentEnemy.name);
                    lookAtEnemy.SetTarget(currentShip);
                }
            }
            else
            {
                // Nếu đây không phải phần tử đầu tiên hợp lệ, gán mục tiêu là previousEnemy
                lookAtEnemy.SetTarget(previousEnemy.NomalEnemyCtrl.transform);
            }

            // Cập nhật previousEnemy
            previousEnemy = currentEnemy;
        }
    }

    public override void DoBehaviorTactic()
    {
        this.SetTargetForUnit();
    }

}
