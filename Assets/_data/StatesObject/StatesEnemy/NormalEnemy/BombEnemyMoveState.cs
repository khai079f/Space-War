using UnityEngine;

public class BombEnemyMoveState : NormalEnemyMoveState
{
    private float attackCooldown = 0.5f;
    private float timeSinceLastAttack = 0f;
    public BombEnemyMoveState(NormalEnemyCtrl normalEnemy, StateMachine stateMachine) : base(normalEnemy, stateMachine)
    {
    }
    public override void Execute()
    {
        base.Execute();
        this.BehaviorEnemy();
    }
    protected override void BehaviorEnemy()
    {
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Far)
        {
            this.normalEnemy.AbilityCtrl.LookAtEnemy.SetOffset(0);
            timeSinceLastAttack = 0f;
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Safe)
        {
            this.normalEnemy.AbilityCtrl.LookAtEnemy.SetOffset(90);
            this.timeSinceLastAttack += Time.deltaTime;
            if (this.timeSinceLastAttack >= this.attackCooldown && this.normalEnemy.AbilityNormalEnemyCtrl.Shootbullet.IsAbilityReady())
            {
                DelayedNormalAttack();
                timeSinceLastAttack = 0f; // Reset thời gian
            }
        }
        if (this.LevelForDistance == CheckDistanceLevel.LevelForDistance.Near || this.LevelForDistance == CheckDistanceLevel.LevelForDistance.canAtk)
        {
            timeSinceLastAttack = 0f;
            this.normalEnemy.AbilityCtrl.LookAtEnemy.SetOffset(0);
            this.ChangeState();
        }
    }
    protected override void ChangeState()
    {
        stateMachine.ChangeState(this.normalEnemy.StateEnemyManager.BombRetreatState);
    }
    private void DelayedNormalAttack()
    {
        this.NormalAttack();
    }

}
