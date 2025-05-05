using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropFly : ParentFly
{
    protected override void ResetValue()
    {
        this.speed = 1f;
        this.direction = Vector3.right;
    }
}
