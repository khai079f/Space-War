using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTargetByMouse : LookAtTarget
{
    protected virtual void Update()
    {
        this.GetMousePos();
    }
    protected virtual void GetMousePos()
    {
        this.targetPos = InputManager.Instance.MouseWorldPos;
        this.targetPos.z = 0f; // Đảm bảo z = 0 trong không gian 2D
    }
}
