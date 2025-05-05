using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : GameMonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] protected AbilityObjectCtrl abilityCtrl;
    public AbilityObjectCtrl AbilityCtrl => abilityCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityCtrl();

    }

    protected virtual void LoadAbilityCtrl()
    {
        if (this.abilityCtrl != null) return;
        this.abilityCtrl = transform.GetComponentInParent<AbilityObjectCtrl>();
        Debug.LogWarning(transform.name + ": LoadAbilityCtrl", gameObject);
    }

}
