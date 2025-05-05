using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticStateMove : TacticBaseState
{
    protected Transform playerPos;
    protected CheckDistanceLevel.LevelForDistance LevelForDistance;
    public TacticStateMove(TacticCtrl tacticCtrl, TacticStateMachine stateMachine, AircraftManagement aircraftManagement) : base(tacticCtrl, stateMachine,aircraftManagement)
    {
        this.aircraftManagement = aircraftManagement;
    }

    public override void Enter()
    {
        base.Enter();
        this.tacticCtrl.FormationCenter.ObjectLook.SetStateLook(true);
        this.tacticCtrl.FormationCenter.ObjectLook.SetOffset(0);
        this.tacticCtrl.FormationCenter.ObjectMove.ResumeMovement();
    }
    public override void Execute()
    {
        this.GetLevelForDistance();
        this.tacticCtrl.FormationCenter.ObjectMove.MoveShip();
        this.BehaviorTactic();
    }

    protected virtual void BehaviorTactic()
    {
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Safe)
        {
            this.NormalAttack();
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.canAtk)
        {
            this.NormalAttack();
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Near)
        {
            this.ChangeState();
        }
    }
    public override void Exit()
    {

    }
    protected virtual void GetLevelForDistance()
    {
        this.playerPos = this.tacticCtrl.FormationCenter.ObjectLook.GetPlayerPos();
        if (this.playerPos == null) return;
        this.LevelForDistance = this.tacticCtrl.FormationCenter.CheckDistanceLevel.GetDistanceLevel(this.tacticCtrl.transform, this.playerPos);
    }
    protected virtual void NormalAttack()
    {
        this.aircraftManagement.UnitUseAbility();

    }
    protected virtual void ChangeState()
    {

        TacticBaseState newState = new TacticStateRetreat(this.tacticCtrl, this.stateMachine, this.aircraftManagement, this.playerPos); // Tạo trạng thái MoveState

        this.stateMachine.ChangeState(newState); // Chuyển sang trạng thái MoveState
    }
}
