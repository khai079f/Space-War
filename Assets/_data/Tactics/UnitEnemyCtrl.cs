using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEnemyCtrl : GameMonoBehaviour
{
    [SerializeField] protected NormalEnemyCtrl normalEnemyCtrl;
    public NormalEnemyCtrl NomalEnemyCtrl => normalEnemyCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseEnemyCtrl();

    }
    protected virtual void LoadBaseEnemyCtrl()
    {
        if (this.transform.childCount == 0)
        {
            this.normalEnemyCtrl = null;
            return;
        }
        if (this.normalEnemyCtrl != null) return;
        this.normalEnemyCtrl = transform.GetComponentInChildren<NormalEnemyCtrl>();
    }
    protected virtual void LoadBaseEnemyCtrl(Transform enemy)
    {
        if (enemy  == null) return;
        this.normalEnemyCtrl = enemy.GetComponent<NormalEnemyCtrl>();
    }
    public virtual void SpawningUnit(string nameUnit)
    {
        this.SpawnNewunit();
        if (this.normalEnemyCtrl != null) return;
        Vector3 pos = this.transform.position;
        Quaternion rot = this.transform.rotation;
        Transform enemy = EnemySpawner.Instance.Spawn(nameUnit, pos, rot);
        if (enemy == null) return;
        this.LoadBaseEnemyCtrl(enemy);
        this.normalEnemyCtrl.transform.SetParent(this.transform);
        this.normalEnemyCtrl.gameObject.SetActive(true); // Activate the object
    }

    public virtual void SetStateForUnit(BaseState baseState)
    {
        this.normalEnemyCtrl.StateEnemy.stateMachine.ChangeState(baseState);
    }
    public virtual void UseAbility()
    {
        this.normalEnemyCtrl.AbilityNormalEnemyCtrl.Shootbullet.Shoot();
    }

    protected virtual void SpawnNewunit()
    {
        if (this.normalEnemyCtrl == null) return;
        if (this.transform.childCount == 0)
        {
            this.normalEnemyCtrl = null;
        }
        
    }


}
