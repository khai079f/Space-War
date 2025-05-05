using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDashFromInput : AbilityDash
{

    protected override void OnUpdate()
    {
        base.OnUpdate();
        this.UpdateKeyDirection();
    }

    protected virtual void UpdateKeyDirection()
    {
        this.keyDirection = InputManager.Instance.Direction;
    }
}
