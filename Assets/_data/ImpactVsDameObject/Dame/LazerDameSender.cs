using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerDameSender : DameSender
{
    [SerializeField] protected BaseAbility baseAbility;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBaseAbility();
    }
    protected virtual void LoadBaseAbility()
    {
        if (baseAbility != null) return;
        this.baseAbility = transform.GetComponentInParent<BaseAbility>();
        Debug.Log(transform.name + ": LoadBaseAbility", gameObject);
    }
    protected virtual void SetValueForLazer(float time =0)
    {
        this.dame = this.baseAbility.AbilityData.Dame/time;

    }
    public virtual void SendDameLazer(Transform dameReceiver,float dameBoost,float time)
    {

        this.dame = this.baseAbility.AbilityData.Dame + dameBoost;
        if (time >0) this.SetValueForLazer(time);
        this.Send(dameReceiver);
    }

}
