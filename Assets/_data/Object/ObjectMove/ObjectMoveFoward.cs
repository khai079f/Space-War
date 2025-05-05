using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveFoward : ObjMovement
{
    [SerializeField] protected Transform targetPosition;
    [SerializeField] protected bool isStopped = true;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTargetPosition();
    }
    protected virtual void LoadTargetPosition()
    {
        if (this.targetPosition != null) return;
        this.targetPosition = transform.Find("Movetarget").GetComponent<Transform>();
        Debug.LogWarning(transform.name+ ": LoadTargetPosition",gameObject);

    }
    // Override phương pháp di chuyển để di chuyển tàu về phía chuột
    public override void MoveShip()
    {
        // Tính toán hướng di chuyển từ vị trí hiện tại tới vị trí chuột
        if (isStopped) return; // Không di chuyển nếu bị dừng
        this.BoostSpeed(this.boost);
        Vector3 direction = (this.targetPosition.position - transform.position).normalized;

        // Di chuyển tàu theo hướng đó với tốc độ đã định sẵn
        transform.parent.parent.position += direction * this.boostSpeed * Time.deltaTime;
    }
    public void MoveShipBackward()
    {
        if (isStopped) return; // Không di chuyển nếu bị dừng
        Vector3 direction = (this.targetPosition.position - transform.position).normalized;
        this.objectMovement.position -= direction * this.speed * Time.deltaTime; // Nhân ngược chiều
    }
    public void StopMovement()
    {
        isStopped = true; // Đặt trạng thái dừng
    }

    public void ResumeMovement()
    {
        isStopped = false; // Bật lại di chuyển
    }
    protected override Transform SetObjectMovement()
    {
        return this.objectMovement = transform.parent.parent;
    }
    public virtual Transform GetTargetMovement()
    {
        return this.targetPosition;
    }
}
