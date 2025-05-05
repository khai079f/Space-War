using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEnemyCtrlAbstract : GameMonoBehaviour
{
    [Header("Ability Enemy Ctrl Abstract")]
    [SerializeField] protected BaseEnemyCtrl baseEnemyCtrl;
    public BaseEnemyCtrl BaseEnemyCtrl => baseEnemyCtrl;
    [SerializeField] protected ObjectLookAtEnemy lookAtEnemy;
    public ObjectLookAtEnemy LookAtEnemy => lookAtEnemy;
    [SerializeField] protected ObjectMoveFoward moveFoward;
    public ObjectMoveFoward MoveFoward => moveFoward;
    [SerializeField] protected CheckDistanceLevel distanceLevel;
    public CheckDistanceLevel DistanceLevel => distanceLevel;
    [SerializeField] protected List<BaseAbility>  listEnemyAbilitys;
    [SerializeField] protected float maxMana = 0;
    [SerializeField] protected float currentMana = 0;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNormalEnemyCtrl();
        this.LoadAbility(ref this.lookAtEnemy, "ObjectLookAtEnemy");
        this.LoadAbility(ref this.moveFoward, "ObjectMoveFoward");
        this.LoadAbility(ref this.distanceLevel, "CheckDistanceLevel");
        this.LoadBaseAbility();
    }
    protected virtual void LoadAbility<T>(ref T component, string componentName) where T : Component
    {
        if (component != null) return;
        component = transform.GetComponentInChildren<T>();
        Debug.LogWarning(transform.name + ": Loaded " + componentName, gameObject);
    }
    public virtual void LoadBaseAbility()
    {
        if (this.listEnemyAbilitys.Count > 0) this.listEnemyAbilitys.Clear();
        this.listEnemyAbilitys.AddRange(GetComponentsInChildren<BaseAbility>());
    }
    protected virtual void LoadNormalEnemyCtrl()
    {
        if (this.baseEnemyCtrl != null) return;
        this.baseEnemyCtrl = transform.GetComponentInParent<BaseEnemyCtrl>();
        Debug.LogWarning(transform.name + ":LoadNormalEnemyCtrl", gameObject);
    }
    protected override void Start()
    {
        base.Start();
        this.Reborn();
        StartCoroutine(ManaRecoveryRoutine());
    }
    protected virtual IEnumerator ManaRecoveryRoutine()
    {
        while (true)
        {
            float time = 0.05f;
            Recove(this.BaseEnemyCtrl.EnemySO.GetRecoveryMana() * time); // Hồi mana
            yield return new WaitForSeconds(time); // Đợi 1 giây
        }
    }

    protected virtual void Reborn()
    {
        this.maxMana = this.BaseEnemyCtrl.EnemySO.GetMana();
        this.currentMana = this.maxMana;
    }

    protected virtual void Recove(float ManaRecovery)
    {
        this.currentMana += ManaRecovery;
        if (this.currentMana > this.maxMana) this.currentMana = this.maxMana;
    }
    public virtual List<BaseAbility> GetListEnemyAbilitys()
    {
        return this.listEnemyAbilitys;
    }
    public virtual bool DeductMana(float deduct)
    {
        if (this.currentMana < deduct)
        {
            //Debug.Log("not enough energy to use skill");
            return false;
        }

        this.currentMana -= deduct;
        return true;
    }
    public virtual void UpdateCurrentMana(float value)
    {
        this.currentMana += value;
    }
    public virtual void UpdateMaxMana(float value)
    {
        this.maxMana += value;
    }
    public virtual float GetCurrentMana()
    {
        return this.currentMana;
    }
    public virtual float GetMaxMana()
    {
        return this.maxMana;
    }
}
