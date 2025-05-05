using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticStateBombMove : TacticStateMove
{
    protected float targetRotation = 90;
    public TacticStateBombMove(TacticCtrl tacticCtrl, TacticStateMachine stateMachine, AircraftManagement aircraftManagement) : base(tacticCtrl, stateMachine, aircraftManagement)
    {
    }
    public override void Enter(){
        base.Enter();
       // Debug.Log("TacticStateBombMove");
    }
    protected override void BehaviorTactic()
    {
        this.GetLevelForDistance();
        BehaviorForUnit();
        if(!this.aircraftManagement.leader.AbilityNormalEnemyCtrl.Shootbullet.IsReady) this.Retreat();
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Far)
        {
            this.aircraftManagement.leader.AbilityCtrl.LookAtEnemy.SetOffset(0);
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Safe)
        {
            this.aircraftManagement.leader.AbilityCtrl.LookAtEnemy.SetOffset(90);
            this.BehaviorSafeForUnit();
        }
        if ( this.LevelForDistance == CheckDistanceLevel.LevelForDistance.canAtk)
        {
            this.Retreat();
        }
    }
    protected virtual void BehaviorForUnit()
    {
       // Debug.Log(this.aircraftManagement.CurrentUnitEnemys.Count);
        for (int i = 0; i < this.aircraftManagement.CurrentUnitEnemys.Count; i++)
        {
            this.aircraftManagement.CurrentUnitEnemys[i].NomalEnemyCtrl.AbilityNormalEnemyCtrl.MoveFoward.MoveShip();
        }
    }
    protected virtual void BehaviorSafeForUnit()
    {
        for (int i = 0; i < this.aircraftManagement.CurrentUnitEnemys.Count; i++)
        {
            this.aircraftManagement.CurrentUnitEnemys[i].NomalEnemyCtrl.AbilityNormalEnemyCtrl.Shootbullet.Shoot();
        }
    }
    protected virtual void Retreat()
    {
        this.aircraftManagement.leader.StateEnemy.stateMachine.ChangeState(this.aircraftManagement.leader.StateEnemyManager.BombRetreatState);
    }
    protected virtual void SetRotationRandom()
    {
        // Xác định khoảng ngẫu nhiên
        if (Random.value > 0.5f) // 50% khả năng
        {
            this.targetRotation = Random.Range(130f, 150f);
        }
        else
        {
            this.targetRotation = Random.Range(-150f, -130f);
        }
    }
    protected override void GetLevelForDistance()
    {
        this.playerPos = PlayerCtrl.instance.CurrentShip.transform;
        if (this.playerPos == null){Debug.Log("null"); return;}
        this.LevelForDistance = this.aircraftManagement.leader.AbilityCtrl.DistanceLevel.GetDistanceLevel(this.aircraftManagement.leader.transform, this.playerPos);
        Debug.Log(this.aircraftManagement.leader.AbilityCtrl.DistanceLevel.GetDistance(this.aircraftManagement.leader.transform.position, this.playerPos.position));
    }

}
