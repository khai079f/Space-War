using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMotherEnemy : AbilityEnemyCtrlAbstract
{
    [Header("Ability Mother Enemy Ctrl")]
    [SerializeField] protected AbilitySummonEnemy abilitySummonEnemy;
    public AbilitySummonEnemy AbilitySummonEnemy => abilitySummonEnemy;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbility(ref this.abilitySummonEnemy, "AbilitySummonEnemy");

    }
}
