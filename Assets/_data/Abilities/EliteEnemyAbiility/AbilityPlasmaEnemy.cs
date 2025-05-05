using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPlasmaEnemy : AbilityEnemyCtrlAbstract
{
    [SerializeField] protected AbilityEnemyAttackLazer enemyAttackLazer;
    public AbilityEnemyAttackLazer EnemyAttackLazer => enemyAttackLazer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbility(ref this.enemyAttackLazer, "AbilityEnemyAttackLazer");
    }
}
