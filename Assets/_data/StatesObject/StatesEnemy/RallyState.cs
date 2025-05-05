using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyState : BaseState
{
    protected NormalEnemyCtrl normalEnemy;
    protected Transform leaderPos;
    protected bool isLeader = false;
    protected bool isReady = false;
    protected int number;
    public RallyState(NormalEnemyCtrl normalEnemy, StateMachine stateMachine, Transform leaderPos = null,bool isLeader=false,int number=0) : base(normalEnemy, stateMachine)
    {
        this.normalEnemy = normalEnemy;
        this.leaderPos = leaderPos;
        this.isLeader = isLeader;
        this.number = number-1;
    }
    // Getter để truy cập isReady
    public bool IsReady()
    {
        return this.isReady;
    }
    public override void Enter()
    {
        if (isLeader) {
            this.ChangeSpeed();
            this.isReady = true;
        }
        else
        {
            Debug.Log(number);
            this.ChangeTarget();
        }

    }
    public override void Execute()
    {
        
        if (this.isLeader) return;
        if (this.leaderPos.gameObject.activeSelf == false) ChangeState();
        this.FollowLeader(this.leaderPos);

    }

    public override void Exit()
    {
        this.normalEnemy.AbilityCtrl.MoveFoward.speed = 5f;
    }

    protected virtual void ChangeSpeed()
    {
        this.normalEnemy.AbilityCtrl.MoveFoward.speed = 4f;
        
    }
    protected virtual void ChangeTarget()
    {
        this.normalEnemy.AbilityCtrl.LookAtEnemy.gameObject.SetActive(true);
    }
    protected virtual void FollowLeader(Transform leaderPos)
    {
        if (this.isLeader) return;
        if (leaderPos == null) return;

        Vector3 offsetPosition = leaderPos.position + leaderPos.right * number; // Thêm độ lệch 1f theo hướng của trục X (right)
        // Sử dụng vị trí có độ lệch làm mục tiêu
        this.normalEnemy.AbilityCtrl.LookAtEnemy.SetTarget(offsetPosition);
        this.IsReady(offsetPosition);
    }
    protected virtual void IsReady(Vector3 leaderPos)
    {
        if (leaderPos == null) return;
        float Distance = this.normalEnemy.AbilityCtrl.DistanceLevel.GetDistance(this.normalEnemy.transform.position, leaderPos);
        if(Distance > 0.1) return;
        this.normalEnemy.AbilityCtrl.MoveFoward.speed = 4f;
        this.isReady = true;


    }
    protected virtual void ChangeState()
    {
        BaseState newState = new RetreatState(this.normalEnemy, this.stateMachine); // Tạo trạng thái MoveState
        stateMachine.ChangeState(newState); // Chuyển sang trạng thái MoveState
    }
}
