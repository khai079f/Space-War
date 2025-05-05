using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXShockwaveDameSender : DameSender
{
    [SerializeField] protected ShockwaveCtrl shockwaveCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShockwaveCtrl();
    }
    protected virtual void LoadShockwaveCtrl()
    {
        if (shockwaveCtrl != null) return;
        this.shockwaveCtrl = transform.GetComponentInParent<ShockwaveCtrl>();
        Debug.Log(transform.name + ": LoadShockwaveCtrl", gameObject);
    }
    protected virtual void SetValueForshockwave()
    {
        if (shockwaveCtrl == null)
        {
            Debug.LogWarning("ShockwaveCtrl is null in " + gameObject.name);
            return;
        }

        if (shockwaveCtrl.AbilityData == null)
        {
            Debug.LogWarning("AbilityData is null in ShockwaveCtrl");
            return;
        }

        this.dame = this.shockwaveCtrl.AbilityData.Dame;
        Debug.Log("dame:"+ dame);
    }
    public override void Send(DameReceiver dameReceiver)
    {
        this.SetValueForshockwave();
        base.Send(dameReceiver);
    }
}
