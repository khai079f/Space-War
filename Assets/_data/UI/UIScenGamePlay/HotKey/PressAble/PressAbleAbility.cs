using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAbleAbility : PressAble
{
    [SerializeField] protected UIAbilityShip UIAbilityShip;

    protected override void Start()
    {
        base.Start();
        this.LoadUIAbilityShip();
    }
    protected virtual void LoadUIAbilityShip()
    {
        if (UIAbilityShip != null) return;
        this.UIAbilityShip = transform.GetComponentInParent<UIAbilityShip>();
        if (UIAbilityShip == null) Debug.LogWarning(transform.name + " Dont able LoadUIAbilityShip ", gameObject);

    }
    public override void PressItem()
    {
        this.UIAbilityShip.OnEnableSkill();
    }

}

