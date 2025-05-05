using UnityEngine;

public class RetreatState : BaseState
{
    protected CheckDistanceLevel.LevelForDistance LevelForDistance;
    protected NormalEnemyCtrl normalEnemy;
    protected Transform playerPos;
    protected float targetRotation ; // Tổng góc quay cần đạt được
    public RetreatState(NormalEnemyCtrl normalEnemy, StateMachine stateMachine) : base(normalEnemy, stateMachine)
    {
        this.normalEnemy = normalEnemy;
        this.playerPos = normalEnemy.AbilityNormalEnemyCtrl.LookAtEnemy.GetPlayerPos();
        this.SetRotationRandom(); // Lấy giá trị ngẫu nhiên
    }

    public override void Enter()
    {
        base.Enter();
          // Debug.Log("RetreatState");
        this.normalEnemy.AbilityCtrl.LookAtEnemy.SetOffset(targetRotation);
    }

    public override void Execute()
    {
        this.normalEnemy.AbilityCtrl.MoveFoward.MoveShip();
        this.GetLevelForDistance();
        if (LevelForDistance == CheckDistanceLevel.LevelForDistance.Far) this.ChangeState();

    }
    public override void Exit()
    {
        //  this.onObj();
    }
    public virtual CheckDistanceLevel.LevelForDistance GetLevelForDistance()
    {
        this.LevelForDistance = this.normalEnemy.AbilityCtrl.DistanceLevel.GetDistanceLevel(this.normalEnemy.transform, this.normalEnemy.AbilityCtrl.LookAtEnemy.ObjectTarget);
        return this.LevelForDistance;
    }
    protected virtual void ChangeState()
    {
        stateMachine.ChangeState(this.normalEnemy.StateEnemyManager.MoveState); // Chuyển sang trạng thái MoveState
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
}
