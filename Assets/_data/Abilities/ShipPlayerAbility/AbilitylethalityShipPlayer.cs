using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitylethalityShipPlayer : AbilityShipAbstract
{
    [Header("Ability lethality Ship Player")]
    [SerializeField] protected DameSender dameSender;
    public DameSender DameSender => dameSender;
    [SerializeField] protected AbilitiesShipImpact AbilityImpact;
    public AbilitiesShipImpact AbilitiesShipImpact => AbilityImpact;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbility(ref this.dameSender, "DameSender", "DameSender");
        this.LoadAbility(ref this.AbilityImpact, "AbilityImpact", "AbilitiesShipImpact");
    }
}
