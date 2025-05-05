using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : GameMonoBehaviour
{
    [SerializeField] protected Vector3 targetPos;
    [SerializeField] protected Transform objectTarget;
    public Transform ObjectTarget =>objectTarget;
    [SerializeField] protected float rotSpeed = 3f;
    [SerializeField] protected bool isLook = true;
    protected Transform parentPos;
    protected float angleOffset;

    protected virtual void FixedUpdate()
    {
        if (!this.isLook) return;
        this.LookAtWithOffset(this.angleOffset);
        if(this.objectTarget != null) this.SetTarget(this.objectTarget);
    }
    public virtual void SetRotSpeed(float speed)
    {
        this.rotSpeed = speed;
    }
    protected virtual void LookTarget()
    {
        this.ParentObject();
        Vector3 diff = this.targetPos - this.parentPos.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        float timeSpeed = this.rotSpeed * Time.fixedDeltaTime;
        Quaternion targetEuler = Quaternion.Euler(0f, 0f, rot_z+90);
        Quaternion currentEuler = Quaternion.Lerp(this.parentPos.rotation, targetEuler, timeSpeed);
        this.parentPos.rotation = currentEuler;
    }
    private void LookAtWithOffset(float angleOffset)
    {
        // Đảm bảo vị trí của đối tượng cha đã được gán
        this.ParentObject();

        // Tính toán vector chênh lệch
        Vector3 diff = this.targetPos - this.parentPos.position;
        diff.Normalize();

        // Tính toán góc quay dựa trên diff và thêm độ lệch
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rot_z += angleOffset; // Thêm độ lệch

        // Tạo rotation mới
        Quaternion targetEuler = Quaternion.Euler(0f, 0f, rot_z + 90);
        float timeSpeed = this.rotSpeed * Time.fixedDeltaTime;
        Quaternion currentEuler = Quaternion.Lerp(this.parentPos.rotation, targetEuler, timeSpeed);

        // Áp dụng rotation cho đối tượng
        this.parentPos.rotation = currentEuler;
    }
    protected virtual Transform ParentObject()
    {
        return this.parentPos = transform.parent;
    }
    public virtual void SetTarget(Transform objectTarget)
    {
        if (objectTarget == null) return;
        this.objectTarget = objectTarget;
        // Sửa giá trị Z của vị trí
        this.targetPos = this.objectTarget.position;
        this.targetPos.z = 0f; // Đảm bảo z = 0 trong không gian 2D
    }
    public virtual void SetTarget(Vector3 objectTarget)
    {
        if (objectTarget == null) return;
        // Sửa giá trị Z của vị trí
        this.targetPos = objectTarget;
        this.targetPos.z = 0f; // Đảm bảo z = 0 trong không gian 2D
    }
    public virtual void SetOffset(float angleOffset)
    {
        this.angleOffset = angleOffset;
    }
    // on/off khả năng nhìn
    public virtual void SetStateLook(bool state)
    {
        this.isLook = state;
    }
}
