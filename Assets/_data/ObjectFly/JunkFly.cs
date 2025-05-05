using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkFly : ParentFly
{
    [SerializeField] protected float minCamPos = -20f;
    [SerializeField] protected float maxCamPos = 20f;
    protected override void ResetValue()
    {
        base.ResetValue();
        this.speed = 2f;
        this.direction = Vector3.right;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.GetFlyDirection();
    }


    protected virtual void GetFlyDirection()
    {
        Vector3 camPos = this.GetCamera();
        Vector3 objPos = transform.parent.position;
        objPos.z += Random.Range(minCamPos, maxCamPos);
        objPos.x += Random.Range(minCamPos, maxCamPos);
        Vector3 diff = camPos - objPos;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.parent.rotation = Quaternion.Euler(0f, 0f, rot_z);

       // Debug.DrawLine(objPos, objPos + diff * 7, Color.red, Mathf.Infinity);
    }

    protected virtual Vector3 GetCamera()
    {
        if (GameCtrl.Instance == null) return Vector3.zero;
        Vector3 camPos = GameCtrl.Instance.MainCamera.transform.position;
        return camPos;
    }
}
