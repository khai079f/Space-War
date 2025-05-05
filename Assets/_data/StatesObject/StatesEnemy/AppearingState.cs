using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearingState : BaseState
{
    protected NormalEnemyCtrl normalEnemy;
    protected AppearBigger appear;

    public AppearingState(NormalEnemyCtrl normalEnemy, StateMachine stateMachine) : base(normalEnemy, stateMachine)
    {
        this.normalEnemy = normalEnemy;
/*         this.appear = this.normalEnemy.AbilityNormalEnemyCtrl.AppearBigger;*/
        if (this.appear == null) Debug.LogError("AppearBigger is null!");

    }

    public override void Enter()
    {
      //  Debug.Log("Enter AppearingState");
        this.EnemyAppear();
    }

    public override void Execute()
    {
        if (this.appear == null) return;

        this.EnemyAppearing();
        this.ChangeState(this.appear.Appeared);
    }

    public override void Exit()
    {
        if (this.appear != null)
        {
            this.EnemyAppeared();
        }
    }

    protected virtual void EnemyAppear()
    {
        this.OffObj();
        this.OnObj();
        this.appear.gameObject.SetActive(true);
        this.appear.InitScale(this.normalEnemy.transform);

    }
    protected virtual void EnemyAppeared()
    {
        this.normalEnemy.Spawner.BackHold(this.normalEnemy.transform);
        this.appear.gameObject.SetActive(false);
        EnemySpawner.Instance.WaitForAppearFinishAndAddHPBar(this.normalEnemy.transform);
        this.appear.ResetAppeared();
    }
    protected virtual void EnemyAppearing()
    {
        this.appear.Appearing(this.normalEnemy.transform);
    }
    protected virtual void ChangeState(bool appeared)
    {
        if (!appeared) return;
        BaseState newState = new NormalEnemyMoveState(this.normalEnemy, this.stateMachine); // Tạo trạng thái MoveState
        stateMachine.ChangeState(newState); // Chuyển sang trạng thái MoveState
    }

    protected virtual void OffObj()
    {
  //      this.normalEnemy.AbilityNormalEnemyCtrl.ObjShooting.gameObject.SetActive(false);
    }
    protected virtual void OnObj()
    {
        this.normalEnemy.AbilityNormalEnemyCtrl.LookAtEnemy.gameObject.SetActive(true);
    }
}
