using System.Collections.Generic;
using UnityEngine;

public class FormationController : GameMonoBehaviour
{
    [SerializeField] protected HolderCtrl holder;
    [SerializeField] protected List<NormalEnemyCtrl> Enemies ;
    [SerializeField] protected int numberMember = 3;
    [SerializeField] protected NormalEnemyCtrl enemyLeader;

    private void FixedUpdate()
    {
        this.LoadEnemies();
        this.ReplaceLeader();
       // this.RallyReady();
        this.Rally();
        this.RallyReady();

    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHolder();

    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.parent.GetComponent<HolderCtrl>();
        Debug.LogWarning(transform.name + " :LoadHolder", gameObject);
    }
    protected virtual void LoadEnemies()
    {
        this.Enemies.RemoveAll(enemy => !enemy.gameObject.activeSelf);
        if (this.numberMember <= this.Enemies.Count) return;
        NormalEnemyCtrl enemy = this.holder.GetEnemy();
        if (enemy == null) return;
        Enemies.Add(enemy);
        this.ChooseLeader();
    }

    protected virtual void ChooseLeader()
    {
        if (this.enemyLeader != null) return;
        if (this.Enemies.Count != this.numberMember) return;
        this.enemyLeader = this.Enemies[0].transform.GetComponent<NormalEnemyCtrl>();
    }
    protected virtual void ReplaceLeader()
    {
        if (this.enemyLeader == null) return;
        if (this.enemyLeader.gameObject.activeSelf) return;
        if (this.Enemies.Count <=0) return;
        this.enemyLeader = this.Enemies[0].transform.GetComponent<NormalEnemyCtrl>();
    }
    protected virtual void Rally()
    {
/*        if (this.Enemies.Count < this.numberMember) return;
        if (this.enemyLeader == null) return;
        int number = 0;
        foreach(NormalEnemyCtrl enemy in Enemies)
        {
            if (enemy.StateCtrl.stateMachine.CurrentState is RetreatState retreatState)
            {
                if (retreatState.GetLevelForDistance() != CheckDistanceLevel.LevelForDistance.Safe) return;
                number += 1;
                this.ChangeStateLeaderRally(enemy);
                this.ChangeStateRally(enemy, number);
            }
        }*/

    }
    protected virtual void RallyReady()
    {
/*        if (this.enemyLeader == null) return;

        bool allReady = true; // Cờ kiểm tra xem tất cả đã sẵn sàng chưa

        foreach (NormalEnemyCtrl enemy in Enemies)
        {
            if (enemy.StateCtrl.stateMachine.CurrentState is RallyState rallyState)
            {
                if (!rallyState.IsReady()) // Nếu có bất kỳ enemy nào chưa sẵn sàng
                {
                    allReady = false;
                    break; // Thoát khỏi vòng lặp, không cần kiểm tra tiếp
                }
            }
        }

        if (allReady) // Nếu tất cả đã sẵn sàng
        {
            foreach (NormalEnemyCtrl enemy in Enemies)
            {
                if (enemy.StateCtrl.stateMachine.CurrentState is RallyState) this.ChangeStateMove(enemy);

            }
        }*/
    }
    protected virtual void ChangeStateMove(NormalEnemyCtrl normalEnemy)
    {
        //if (normalEnemy != this.enemyLeader) return;
/*        BaseState newState = new NormalEnemyMoveState(normalEnemy, normalEnemy.StateCtrl.stateMachine);
        normalEnemy.StateCtrl.stateMachine.ChangeState(newState);*/
    }
    protected virtual void ChangeStateRally(NormalEnemyCtrl normalEnemy, int number)
    {
/*        if (normalEnemy == this.enemyLeader) return;
        BaseState newState = new RallyState(normalEnemy, normalEnemy.StateCtrl.stateMachine, this.enemyLeader.transform,false, number);
        normalEnemy.StateCtrl.stateMachine.ChangeState(newState);*/
    }
    protected virtual void ChangeStateLeaderRally(NormalEnemyCtrl normalEnemy)
    {
/*        if (normalEnemy != this.enemyLeader) return;
        BaseState newState = new RallyState(normalEnemy, normalEnemy.StateCtrl.stateMachine,null ,true);
        normalEnemy.StateCtrl.stateMachine.ChangeState(newState);*/
    }
   

}
