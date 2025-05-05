using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShipDameSender : DameSender
{
    [SerializeField] protected AbilityShipAbstract abilityShip;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityShip();
    }
    protected virtual void LoadAbilityShip()
    {
        if (abilityShip != null) return;
        this.abilityShip = transform.parent.GetComponent<AbilityShipAbstract>();
        Debug.Log(transform.name + ": LoadAbilityShip", gameObject);
    }
    protected override void SetValueForAbility()
    {
        this.dame = this.abilityShip.AbilityData.Dame + this.abilityShip.ShipPlayAbleAbilities.ShipCtrl.ShipProfileSO.BaseDame;
    }
    public override void Send(DameReceiver dameReceiver)
    {
        base.Send(dameReceiver);
    }

}
