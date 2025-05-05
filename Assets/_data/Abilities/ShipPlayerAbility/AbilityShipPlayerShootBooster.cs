using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShipPlayerShootBooster : AbilityShipPlayerShoot
{
    [SerializeField] protected bool isBoosterDame = false;
    public override void Shooting()
    {
        this.ChangeBullet();
        base.Shooting();

    }
    public override void Active()
    {
        base.Active();
        if (this.CheckBeforeBooster() && this.isBoosterDame) this.shipPlayAbleAbilities.DeductMana(this.abilityData.ManaUse);
    }
    protected virtual void ChangeBullet()
    {
        if (!this.CheckBeforeBooster())
        {
            this.SetNameBullet(BulletSpawner.bulletTwo);
            return;
        }
        if (this.isBoosterDame)
        {
            this.SetNameBullet(BulletSpawner.bulletThree);
        }
        else
        {
            this.SetNameBullet(BulletSpawner.bulletTwo);
        }
        
    }
    public virtual void SetStatusBoosterDame(bool state)
    {
        this.isBoosterDame = state;
    }
    private bool CheckBeforeBooster()
    {
        if (this.shipPlayAbleAbilities.GetCurrentMana() <= this.abilityData.ManaUse) return false;
        return true;
    }

}
