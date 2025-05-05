using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShipEnergyShield : AbilityShipAbstract
{
    [Header("Shield Properties")]
    [SerializeField] protected float recoveryTime;
    [SerializeField] private AbilityDameReceiver abilityDameReceiver;

    protected override void Start()
    {
        base.Start();
        this.SetHPforShield();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityDameReceiver();
    }
    protected virtual void LoadAbilityDameReceiver()
    {
        if (this.abilityDameReceiver == null) this.abilityDameReceiver = transform.GetComponentInChildren<AbilityDameReceiver>(); 
        this.UpdateSizeForShield();
    }
    protected override void Timing()
    {
        if (this.abilityDameReceiver == null) return;
        this.EnergyShieldRegeneration();

    }
    protected virtual void EnergyShieldRegeneration()
    {
        if (this.abilityDameReceiver.IsDead())
        {
            if (this.isReady)
            {
                if (this.shipPlayAbleAbilities.GetCurrentMana() < this.abilityData.ManaUse) return;
                this.shipPlayAbleAbilities.DeductMana(this.abilityData.ManaUse);
                this.abilityDameReceiver.StatusActive(true);
                this.Active();
            }
            this.timer += Time.fixedDeltaTime;
            if (this.timer < this.delay) return;
            this.isReady = true;
        }

    }
    protected virtual void SetHPforShield()
    {
        if (this.abilityDameReceiver == null) return;
        this.abilityDameReceiver.SetCurrentMaxHp(this.abilityData.Dame);
        this.abilityDameReceiver.SetCurrentHp(this.abilityData.Dame);
    }
    public float ShieldTakeDamage(float damage)
    {
        if (this.abilityDameReceiver == null || this.abilityDameReceiver.IsDead()) return damage;
        if (damage >= this.abilityDameReceiver.GetCurrentHp())
        {
            float remainingDamage = damage - this.abilityDameReceiver.GetCurrentHp(); // Sát thương còn dư
            this.abilityDameReceiver.SetCurrentHp(0); // Khiên bị phá
            return remainingDamage; // Trả lại sát thương còn dư
        }
        else
        {
            //Debug.Log("ShieldTakeDamage:" + 0);
            return 0; // Không còn dư sát thương
        }
    }

    protected virtual void UpdateSizeForShield()
    {
        if (this.abilityDameReceiver == null) return;
        this.abilityDameReceiver.UpdateLocalScale(this.shipPlayAbleAbilities.ShipCtrl.ShipModelCtrl.GetShipDiameter());
        this.abilityDameReceiver.UpdateRadiusCollider(this.shipPlayAbleAbilities.ShipCtrl.ShipModelCtrl.GetShipDiameter());
    }

}
