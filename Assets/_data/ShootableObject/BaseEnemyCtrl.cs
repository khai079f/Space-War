using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyCtrl : BaseShipCtrl
{
    [Header("Base Enemy Ctrl")]
    [SerializeField] protected AbilityEnemyCtrlAbstract abilityCtrl;
    public AbilityEnemyCtrlAbstract AbilityCtrl => abilityCtrl;

    [SerializeField] protected ShootableObjectSO enemySO;
    public ShootableObjectSO EnemySO => enemySO;

    [SerializeField] protected Spawner spawner;
    public Spawner Spawner => spawner;

    protected StateEnemyCtrl stateEnemy;
    public StateEnemyCtrl StateEnemy => stateEnemy;

    [SerializeField] protected ModelCtrl model;
    [SerializeField] protected string stateMachine;
    public ModelCtrl enemyModel => model;

    // Thêm thuộc tính quản lý state cho enemy
    protected StateEnemyManager stateEnemyManager;
    public StateEnemyManager StateEnemyManager => stateEnemyManager;

    protected override void Awake()
    {
        base.Awake();
        // Khởi tạo stateEnemy
        this.stateEnemy = new StateEnemyCtrl();
        // Lưu ý: Chỉ các enemy kiểu NormalEnemyCtrl mới khởi tạo StateEnemyManager
        if (this is NormalEnemyCtrl normalEnemy)
        {
            // Giả sử rằng playerPos lấy từ PlayerCtrl.instance, nếu có
            stateEnemyManager = new StateEnemyManager(normalEnemy, this.stateEnemy.stateMachine);
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (this.stateEnemy == null || this.stateEnemy.stateMachine == null)
        {
            this.InitializeStateForEnemy(this.GetState());
            return;
        }
        if (this.stateEnemy.stateMachine.CurrentState is DeadState)
        {
            this.stateEnemy.stateMachine.ChangeState(this.GetState());
        }
        else
        {
            this.InitializeStateForEnemy(this.GetState());
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSO();
        this.LoadSpawner();
        this.LoadComponentInChildren(ref this.model, "ModelCtrl");
        this.LoadComponentInChildren(ref this.abilityCtrl, "AbilityEnemyCtrlAbstract");
    }

    protected virtual void LoadComponentInChildren<T>(ref T component, string componentName) where T : Component
    {
        if (component != null) return;
        component = transform.GetComponentInChildren<T>();
        Debug.LogWarning(transform.name + ": Loaded " + componentName, gameObject);
    }

    protected virtual void LoadSO()
    {
        if (this.enemySO != null) return;
        string resPath = "ShootableObject/" + this.GetObjTypeString() + "/" + transform.name;
        this.enemySO = Resources.Load<ShootableObjectSO>(resPath);
        Debug.Log(transform.name + ": LoadSO", gameObject);
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponentInParent<Spawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    private void Update()
    {
        if (this.stateEnemy == null) return;
        this.stateEnemy.stateMachine.ExecuteState();
    }

    public virtual void InitializeStateForEnemy(BaseState baseState)
    {
        if (this.stateEnemy.stateMachine != null) return;
        if (baseState == null)
        {
            Debug.LogError("StateCtrl is not initialized.", this);
            return;
        }
        this.stateEnemy.InitializeStateMachine(baseState);
    }

    private void FixedUpdate()
    {
        if (this.stateEnemy?.stateMachine?.CurrentState == null) return;
        this.stateMachine = this.stateEnemy.stateMachine.CurrentState.ToString();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.stateEnemy.stateMachine.ChangeState(new DeadState(this, this.stateEnemy.stateMachine));
    }

    public abstract BaseState GetState();
    protected abstract string GetObjTypeString();
}
