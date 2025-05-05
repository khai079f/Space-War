using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIAbilityHotKeyAbstract : GameMonoBehaviour
{
    [SerializeField] protected UIAbilityShip UIAbilityShip;
    protected BaseAbility baseAbility;
    protected override void Start()
    {
        base.Start();
        this.LoadUIAbilityShip();
        if (this.UIAbilityShip == null) Debug.LogWarning("this.UIAbilityShip == null", gameObject);
        this.baseAbility = this.UIAbilityShip.BaseAbility;
    }
    protected virtual void LoadUIAbilityShip()
    {
        if (UIAbilityShip != null) return;
        this.UIAbilityShip = transform.GetComponentInParent<UIAbilityShip>();
        if (UIAbilityShip == null) Debug.LogWarning(transform.name + " Dont able LoadUIAbilityShip ", gameObject);
    }
}
