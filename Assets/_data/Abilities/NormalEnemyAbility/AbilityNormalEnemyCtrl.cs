using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityNormalEnemyCtrl : AbilityEnemyCtrlAbstract
{
    [Header("Ability Normal Enemy Ctrl")]
    [SerializeField] protected AbilityShootbullet shootbullet;
    public AbilityShootbullet Shootbullet => shootbullet;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbility(ref this.shootbullet, "AbilityShootbullet");
    }
}
