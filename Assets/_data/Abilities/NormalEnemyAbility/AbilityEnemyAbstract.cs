using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEnemyAbstract : BaseAbility
{
    [Header("Ability EnemyAbstract")]
    [SerializeField] protected AbilityEnemyCtrlAbstract abilityEnemyCtrl;
    public AbilityEnemyCtrlAbstract AbilityEnemyCtrl => abilityEnemyCtrl;

    protected override void Start()
    {
        base.Start();
        this.delay = this.abilityData.Cooldown;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityNormalEnemyCtrl();
    }

    protected virtual void LoadAbilityNormalEnemyCtrl()
    {
        if (this.abilityEnemyCtrl != null) return;
        this.abilityEnemyCtrl = transform.GetComponentInParent<AbilityEnemyCtrlAbstract>();
    }

    protected override void LoadAbilityData()
    {
        if (this.abilityData != null) return;
        string resPath = "ShootableObject/Enemy/Ability/" + transform.name;
        this.abilityData = Resources.Load<AbilityData>(resPath);
        Debug.LogWarning(transform.name + ": LoadAbilityData for Enemy", gameObject);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.abilityData.SetBaseValue();
    }
}
