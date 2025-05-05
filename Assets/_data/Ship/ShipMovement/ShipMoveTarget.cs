using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoveTarget : ObjMovement
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float distance = 1f;
    [SerializeField] protected float minDistance = 1f;

/*    protected override void Moving()
    {
        this.distance = Vector3.Distance(transform.position, this.targetPosition);
        if (this.distance < minDistance) return;
        Vector3 direction = (targetPosition - transform.parent.position).normalized;
        Vector3 newPos = transform.parent.position + direction * moveSpeed * Time.deltaTime;
        transform.parent.position = newPos;
    }
    public virtual void setTarget(Transform target)
    {
        this.target = target;
    }*/
}

