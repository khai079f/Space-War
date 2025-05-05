using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTargetLimitDegrees : ObjectLookAtEnemy
{
    [SerializeField] protected bool Left; // Giới hạn góc quay (có thể âm hoặc dương)
    protected override void LookTarget()
    {
        base.LookTarget();
    }
    protected override Transform ParentObject()
    {
        return this.parentPos = transform;
    }
}
