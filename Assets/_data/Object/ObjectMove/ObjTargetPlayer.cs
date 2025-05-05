using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTargetPlayer : ObjMovement
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected float distance = 1f;
    [SerializeField] protected float minDistance = 1f;

/*    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTargetPlayer();
    }
*//*    protected override void Moving()
    {
        this.distance = Vector3.Distance(transform.position, this.targetPosition);
        if (this.distance < minDistance) return;
        Vector3 direction = (targetPosition - transform.parent.position).normalized;
        Vector3 newPos = transform.parent.position + direction * moveSpeed * Time.deltaTime;
        transform.parent.position = newPos;
    }

    protected virtual void LoadTargetPlayer()
    {
        if (this.player != null) return;
        this.player = GameObject.FindGameObjectWithTag("Player");
        Debug.LogWarning(transform.name + ": LoadTargetPlayer", gameObject);

    }*/

}