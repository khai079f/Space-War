using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticStateRetreat : TacticBaseState
{
    protected CheckDistanceLevel.LevelForDistance LevelForDistance;
    protected Transform playerPos;
    protected float targetRotation = 150f; // Tổng góc quay cần đạt được
    public TacticStateRetreat(TacticCtrl tacticCtrl, TacticStateMachine stateMachine, AircraftManagement aircraftManagement, Transform playerPos) : base(tacticCtrl, stateMachine, aircraftManagement)
    {
        this.playerPos = playerPos;
    }
    public override void Enter()
    {
        base.Enter();
        //   Debug.Log("RetreatState");
        this.tacticCtrl.FormationCenter.ObjectLook.SetStateLook(false);
        this.tacticCtrl.FormationCenter.ObjectMove.SetValueBoost(2);
        // this.tacticCtrl.FormationCenter.ObjectLook.SetOffset(targetRotation);
    }

    public override void Execute()
    {
        this.tacticCtrl.FormationCenter.ObjectMove.MoveShip();
        this.GetLevelForDistance();
        if (LevelForDistance == CheckDistanceLevel.LevelForDistance.Far) this.ChangeState();
    }

    public override void Exit()
    {
        this.tacticCtrl.FormationCenter.ObjectMove.SetValueBoost(1);
    }
    protected virtual void GetLevelForDistance()
    {
        this.playerPos = this.tacticCtrl.FormationCenter.ObjectLook.GetPlayerPos();
        if (this.playerPos == null) return;
        this.LevelForDistance = this.tacticCtrl.FormationCenter.CheckDistanceLevel.GetDistanceLevel(this.tacticCtrl.transform, this.playerPos);
    }
    protected virtual void ChangeState()
    {

        TacticBaseState newState = this.tacticCtrl.GetStateForTactic(); // Tạo trạng thái MoveState

        this.stateMachine.ChangeState(newState); // Chuyển sang trạng thái MoveState
    }
}
