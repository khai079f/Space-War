using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TacticCtrl : GameMonoBehaviour
{
    [SerializeField] protected FormationCenterCtrl formationCenter;
    public FormationCenterCtrl FormationCenter => formationCenter;
    [SerializeField] protected AircraftManagement aircraftManagement;
    public AircraftManagement AircraftManagement => aircraftManagement;
    [SerializeField] protected TacticDespawn despawn;
    public TacticDespawn Despawn => despawn;
    [SerializeField] protected List<string> nameEnemy = new List<string>();
    public List<string> NameEnemy => nameEnemy;
    protected StateTacticCtrl stateTactic;
    public StateTacticCtrl StateTactic => stateTactic;
    protected override void Awake()
    {
        base.Awake();
        this.stateTactic = new StateTacticCtrl();
    }
    protected override void Start()
    {
        base.Start();
        this.SetNameEnemyUnit();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (this.stateTactic == null || this.stateTactic.stateMachine == null)
        {
            this.InitializeStateForEnemy(this.GetStateForTactic());
            return;
        }
        if (this.stateTactic.stateMachine.CurrentState is TacticStateDissolution)
        {
            this.stateTactic.stateMachine.ChangeState(this.GetStateForTactic());
            this.aircraftManagement.SpawningvsStatesForUnit();
        }
        else
        {
            this.InitializeStateForEnemy(this.GetStateForTactic());
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbility(ref this.formationCenter, "FormationCenterCtrl");
        this.LoadAbility(ref this.aircraftManagement, "AircraftManagement");
        this.LoadAbility(ref this.despawn, "TacticDespawn");

    }
    protected virtual void LoadAbility<T>(ref T component, string componentName) where T : Component
    {
        if (component != null) return;
        component = transform.GetComponentInChildren<T>();
        Debug.LogWarning(transform.name + ": Loaded " + componentName, gameObject);
    }

    private void Update()
    {
        if (this.stateTactic == null) return;
        this.stateTactic.stateMachine.ExecuteState();

    }
    public virtual void InitializeStateForEnemy(TacticBaseState baseState)
    {
        if (this.stateTactic.stateMachine != null) return;
        if (baseState == null)
        {
            Debug.LogError("StateCtrl is not initialized.", this);
            return;
        }
        this.stateTactic.InitializeStateMachine(baseState);

    }
    public virtual void TacticDissolution()
    {
        TacticBaseState newState = new TacticStateDissolution(this, this.stateTactic.stateMachine, this.aircraftManagement);
        this.stateTactic.stateMachine.ChangeState(newState);
    }
    protected virtual void SetNameEnemyUnit()
    {
        if ( this.nameEnemy.Count != 0) return; 
        this.nameEnemy.Add(EnemySpawner.Instance.Prefabs[0].name);
    }
    public abstract void DoBehaviorTactic();
    public abstract TacticBaseState GetStateForTactic();

}
