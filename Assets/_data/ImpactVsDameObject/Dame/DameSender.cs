using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameSender : GameMonoBehaviour
{
    [SerializeField] protected float dame = 1;
    protected override void Start()
    {
        base.Start();
        this.SetValueForAbility();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    protected virtual void SetValueForAbility()
    {
        //
    }
    public virtual void Send(Transform obj)
    {
        DameReceiver dameReceiver = obj.GetComponentInChildren<DameReceiver>();
        if (dameReceiver == null) return;
        this.Send(dameReceiver);
    }
    public virtual void Send(DameReceiver dameReceiver)
    {
        dameReceiver.Deduct(this.dame);
    }


}
