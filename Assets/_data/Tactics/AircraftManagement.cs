using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftManagement : GameMonoBehaviour
{
    [SerializeField] protected List<UnitEnemyCtrl> unitEnemys = new List<UnitEnemyCtrl>();
    [SerializeField] protected TacticCtrl tacticCtrl;
    [SerializeField] protected List<UnitEnemyCtrl> currentUnitEnemys ;
    public List<UnitEnemyCtrl> CurrentUnitEnemys => currentUnitEnemys;
    public NormalEnemyCtrl leader;
    protected int numberUnits = 0;
    protected int numberUnitsForTactic = 2;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAllUnitEnemyCtrl();
        this.LoadTacticCtrl();
    }
    protected virtual void LoadAllUnitEnemyCtrl()
    {
        if (this.unitEnemys.Count != 0) return;
        UnitEnemyCtrl[] unitEnemyCtrls = transform.GetComponentsInChildren<UnitEnemyCtrl>();
        this.unitEnemys.AddRange(unitEnemyCtrls);
        Debug.LogWarning(transform.name + " LoadAllUnitEnemyCtrl",gameObject);
    }
    protected virtual void LoadTacticCtrl()
    {
        if (this.tacticCtrl != null) return;
        this.tacticCtrl = transform.GetComponentInParent<TacticCtrl>();
        Debug.LogWarning(transform.name + " LoadTacticCtrl", gameObject);
    }
    protected override void Start()
    {
        base.Start();
        this.SpawningvsStatesForUnit();

    }
    public virtual void SpawningvsStatesForUnit()
    {
        this.currentUnitEnemys = new List<UnitEnemyCtrl>();
        if (this.unitEnemys == null || this.unitEnemys.Count <= this.numberUnitsForTactic) return;
        foreach (UnitEnemyCtrl unitEnemy in this.unitEnemys)
        {
           // Debug.Log(unitEnemy.name);
            unitEnemy.SpawningUnit(GetNameUnit());
            if (unitEnemy.NomalEnemyCtrl == null) return;
            //
            this.RegisterUnit(unitEnemy);

            unitEnemy.SetStateForUnit(new TacticState(unitEnemy.NomalEnemyCtrl, unitEnemy.NomalEnemyCtrl.StateEnemy.stateMachine));
        }
        this.currentUnitEnemys.AddRange(unitEnemys);
        this.numberUnits = this.unitEnemys.Count;
        this.leader = this.unitEnemys[0].NomalEnemyCtrl;
        // Gọi sự kiện sau khi hoàn thành
        this.tacticCtrl.DoBehaviorTactic();

    }

    protected virtual void UpdateNumberUnits()
    {
        if (this.currentUnitEnemys == null || this.currentUnitEnemys.Count <= this.numberUnitsForTactic) return;

        for (int i = this.currentUnitEnemys.Count - 1; i >= 0; i--)
        {
            UnitEnemyCtrl unit = this.currentUnitEnemys[i];
            if (unit == null || unit.transform.childCount==0)
            {
                this.currentUnitEnemys.RemoveAt(i);
                this.UnregisterUnit(unit);
            }
        }
        this.leader = this.currentUnitEnemys[0].NomalEnemyCtrl;
        this.numberUnits = this.currentUnitEnemys.Count;
        this.tacticCtrl.DoBehaviorTactic();
        if (this.numberUnits <= this.numberUnitsForTactic) this.DissolutionUnit();
    }
    public virtual void UnitUseAbility()
    {
        if (this.currentUnitEnemys == null || this.currentUnitEnemys.Count <= this.numberUnitsForTactic) return;
        foreach (UnitEnemyCtrl unitEnemy in this.currentUnitEnemys)
        {
            unitEnemy.UseAbility();
        }
    }

    protected virtual void DissolutionUnit()
    {
        if (this.currentUnitEnemys == null || this.currentUnitEnemys.Count == 0) this.tacticCtrl.TacticDissolution();
       // Debug.Log(this.currentUnitEnemys.Count);
        foreach (UnitEnemyCtrl unitEnemy in this.currentUnitEnemys)
        {
            unitEnemy.SetStateForUnit(new NormalEnemyMoveState(unitEnemy.NomalEnemyCtrl, unitEnemy.NomalEnemyCtrl.StateEnemy.stateMachine));
            unitEnemy.NomalEnemyCtrl.transform.SetParent(EnemySpawner.Instance.Holder);
        }
        this.tacticCtrl.TacticDissolution();
    }
    protected void UnregisterUnit(UnitEnemyCtrl unit)
    {
        unit.NomalEnemyCtrl.DameReceiver.OnDeath -= UpdateNumberUnits; // Hủy đăng ký sự kiện OnDeath
    }
    protected void RegisterUnit(UnitEnemyCtrl unit)
    {
        unit.NomalEnemyCtrl.DameReceiver.OnDeath += UpdateNumberUnits; // Đăng ký lắng nghe sự kiện OnDeath
    }

    protected virtual string GetNameUnit()
    {
        return this.tacticCtrl.NameEnemy[0];
    }
}
