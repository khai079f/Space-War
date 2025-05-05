using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDameSender : DameSender
{
    public virtual void Send(Transform obj, Collider2D collider)
    {
        ShipDameReceiver dameReceiver = obj.GetComponentInChildren<ShipDameReceiver>();
        if (dameReceiver == null) return;
        this.Send(dameReceiver);
        if(transform.gameObject.activeSelf) dameReceiver.ApplyKnockback(collider);


    }
}
