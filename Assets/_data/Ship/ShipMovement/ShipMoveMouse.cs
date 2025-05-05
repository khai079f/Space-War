using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoveMouse : ObjMovement
{
    [SerializeField] protected Vector3 targetPosition; // Vị trí mục tiêu (vị trí chuột trong thế giới)

    protected virtual void Update()
    {
        // Cập nhật vị trí chuột mỗi frame
        this.GetMousePosition();

        // Di chuyển tàu về phía chuột
        this.MoveShip();
    }

    // Lấy vị trí chuột trong không gian thế giới
    protected virtual void GetMousePosition()
    {
        this.targetPosition = InputManager.Instance.MouseWorldPos;
        this.targetPosition.z = 0f;
    }

    // Override phương pháp di chuyển để di chuyển tàu về phía chuột
    public override void MoveShip()
    {
        // Tính toán hướng di chuyển từ vị trí hiện tại tới vị trí chuột
        Vector3 direction = (this.targetPosition - transform.position).normalized;

        // Di chuyển tàu theo hướng đó với tốc độ đã định sẵn
        transform.parent.position += direction * this.speed * Time.deltaTime;
    }
}
